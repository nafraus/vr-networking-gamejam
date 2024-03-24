using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Threading;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using Unity.Netcode;
using Unity.Services.Relay;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class Gun : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private XRDirectInteractor interactor;
    [SerializeField] private InputActionProperty fireReference;
    [SerializeField] private GunsSO gunReferences;
    [SerializeField] private Transform shootingOrigin;
    [SerializeField] private PlayerScore playerScore;
    [SerializeField] private GameObject tracerPrefab;
    [SerializeField] private LayerMask bulletRaycastingLayers;
    #endregion

    [Foldout("Events")][SerializeField] private UnityEvent OnFireEvent;
    [Foldout("Events")][SerializeField] private UnityEvent OnEmptyMagEvent;
    [Foldout("Events")][SerializeField] private UnityEvent OnReloadEvent;

    //Effects Dictionary / List

    private int currentClipCount;
    private float timeSinceLastShot;
    private float timeSinceLastBurstShot;
    private bool hasAlreadyFired = false;
    private GunSettingsSO gun => gunReferences.activeGun;


    #region Properties
    public float FireThreshold
    {
        get => gun.fireThreshhold - gun.GetModifierValueModifierType(ModifierType.ReduceTriggerThreshhold);
    }
    public float BurstSize
    {
        get => gun.burstSize + gun.GetModifierValueModifierType(ModifierType.AddBurstCount);
    }
    public float BurstRechamberTime
    {
        get => gun.burstRechamberTime - gun.GetModifierValueModifierType(ModifierType.ReduceBurstRechamberTime);
    }
    public float RaycastRadius
    {
        get => gun.raycastRadius + gun.GetModifierValueModifierType(ModifierType.IncreaseBulletRadius);
    }
    public float SpreadStrength
    {
        get => gun.spreadStrength - gun.GetModifierValueModifierType(ModifierType.ReduceSpreadValue);
    }
    public float ClipSize
    {
        get => gun.clipSize + gun.GetModifierValueModifierType(ModifierType.AddClipSize);
    }
    public float FireRateTime
    {
        get => gun.fireRateTime - gun.GetModifierValueModifierType(ModifierType.ReduceFireRate);
    }

    #endregion

    void Start()
    {
        gunReferences.SetCurrentGun(GunsSO.SetGunType.Default);
        gunReferences.SetActiveGun(GunsSO.SetGunType.CurrentGameGun);

        currentClipCount = (int)ClipSize;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.fixedDeltaTime;
        bool actionHeld = fireReference.action.ReadValue<float>() > FireThreshold;

        if (!actionHeld) hasAlreadyFired = false;

        if (actionHeld && hasAlreadyFired) return;

        if(actionHeld && !hasAlreadyFired)
        {
            hasAlreadyFired = true;

            bool fireIsValid = ValidateFire();

            if (fireIsValid)
            {
                DoFire();
            }
        }
    }

    #region Firing Gun Behavior / Checks
    bool ValidateFire()
    {
        if (currentClipCount == 0)
        {
            DoFireFailed();
            return false;
        }

        if (timeSinceLastShot < FireRateTime) return false;

        return true;
    }

    void DoFire()
    {
        timeSinceLastShot = 0;
        //Expend an ammo
        currentClipCount--;

        //Fire, single or burst shot
        if (BurstSize == 1) FireOnce();
        else StartCoroutine(FireMultiple());
    }

    void FireOnce()
    {
        Debug.Log("Shots fired");
        interactor.SendHapticImpulse(0.85f, 0.25f);
        OnFireEvent.Invoke();

        if(gun.spreadStrength == 0) RaycastShot(transform.forward);
        else
        {
            float randX = UnityEngine.Random.Range(-SpreadStrength, SpreadStrength);
            float randY = UnityEngine.Random.Range(-SpreadStrength, SpreadStrength);
            Vector3 spread = transform.forward +
                transform.right * randX +
                transform.up * randY;
            RaycastShot(spread);
        }
    }

    void RaycastShot(Vector3 direction)
    {
        RaycastHit hit;
        Physics.SphereCast(shootingOrigin.position, RaycastRadius, direction, out hit, 100, bulletRaycastingLayers);

        GameObject tracerOBJ = Instantiate(tracerPrefab);
        BulletTracer tracer = tracerOBJ.GetComponent<BulletTracer>();
        if (hit.collider)
        {
            tracer.Init(shootingOrigin.position, hit.point, RaycastRadius);
        }
        else
        {
            tracer.Init(shootingOrigin.position, shootingOrigin.position + direction * 25, RaycastRadius);
        }

        //Look for target
        if (hit.collider)
        {
            int layer = hit.collider.gameObject.layer;
            switch (layer)
            {
                case 6:
                    
                    NetworkTarget target = hit.collider.GetComponent<NetworkTarget>();
                    if (target)
                    {
                        target.SetPlayerScore(playerScore);
                        target.TargetHitServer();
                    }
                    break;
                case 9:
                    if (!gun.isUIMode) break;
                    ShootableButton button = hit.collider.GetComponent<ShootableButton>();
                    if(button) button.Activate();
                    break;
            }
        }


        //Last to make sure tracer is initialized
        if(NetworkManager.Singleton.IsConnectedClient) tracer.NetworkObject.Spawn(false);
    }

    IEnumerator FireMultiple()
    {
        int bulletsFired = 0;
        timeSinceLastBurstShot = BurstRechamberTime;

        while (bulletsFired != BurstSize)
        {
            if(timeSinceLastBurstShot >= BurstRechamberTime)
            {

                FireOnce();
                bulletsFired++;
                timeSinceLastBurstShot = 0;
            }

            timeSinceLastBurstShot += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
    }

    void DoFireFailed()
    {
        Debug.Log("FireFailed");
        //Code failed for if a fire does not happen
        OnEmptyMagEvent.Invoke();
        interactor.SendHapticImpulse(0.05f, 0.1f);
    }
    #endregion

    #region Reload
    public void Reload()
    {
        OnReloadEvent.Invoke();
        currentClipCount = 0;
        StartCoroutine(ReloadLoop());
    }

    IEnumerator ReloadLoop()
    {
        yield return new WaitForSeconds(gun.reloadTime);
        currentClipCount = (int) ClipSize;
    }
    #endregion


    public void TurnOnCurrentGun()
    {
        if(gunReferences.activeGun != gunReferences.uiGun)
        {
            gunReferences.SetActiveGun(GunsSO.SetGunType.UI);
        }
        else gunReferences.SetActiveGun(GunsSO.SetGunType.CurrentGameGun);

        Reload();
        currentClipCount = (int)ClipSize;
    }
}

