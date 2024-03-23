using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Threading;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class Gun : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private XRDirectInteractor interactor;
    [SerializeField] private InputActionProperty fireReference;
    [SerializeField] private GunSettingsSO gun;
    [SerializeField] private Transform shootingOrigin;
    [SerializeField] private PlayerScore playerScore;
    #endregion

    [Foldout("Events")][SerializeField] private UnityEvent OnFireEvent;
    [Foldout("Events")][SerializeField] private UnityEvent OnEmptyMagEvent;
    [Foldout("Events")][SerializeField] private UnityEvent OnReloadEvent;

    //Effects Dictionary / List

    private int currentClipCount;
    private float timeSinceLastShot;
    private float timeSinceLastBurstShot;
    private bool hasAlreadyFired = false;

    void Start()
    {
        currentClipCount = gun.clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.fixedDeltaTime;
        bool actionHeld = fireReference.action.ReadValue<float>() > gun.fireThreshhold;

        if (!actionHeld) hasAlreadyFired = false;

        if (actionHeld && hasAlreadyFired) return;

        if(actionHeld && !hasAlreadyFired)
        {
            hasAlreadyFired = true;

            Debug.Log("Fire Called");
            bool fireIsValid = ValidateFire();

            if (fireIsValid)
            {
                DoFire();
            }
        }
    }

    bool ActionCheck() 
    {
        return fireReference.action.ReadValue<float>() < gun.fireThreshhold;
    }

    IEnumerator UpdateLoop()
    {
        while (true)
        {
            yield return new WaitUntil(ActionCheck);
        }
    }

    bool ValidateFire()
    {
        if (currentClipCount == 0)
        {
            DoFireFailed();
            return false;
        }

        if (timeSinceLastShot < gun.fireRateTime) return false;

        return true;
    }

    void DoFire()
    {
        timeSinceLastShot = 0;
        //Expend an ammo
        currentClipCount--;

        //Fire, single or burst shot
        if (gun.burstSize == 1) FireOnce();
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
            float randX = UnityEngine.Random.Range(-gun.spreadStrength, gun.spreadStrength);
            float randY = UnityEngine.Random.Range(-gun.spreadStrength, gun.spreadStrength);
            Vector3 spread = transform.forward +
                transform.right * randX +
                transform.up * randY;
            RaycastShot(spread);
        }
    }

    void RaycastShot(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(shootingOrigin.position,direction);
        Physics.Raycast(ray, out hit);

        //Prototype Laser
        GameObject lineRend = new GameObject();
        LineRenderer line = lineRend.AddComponent<LineRenderer>();
        line.SetPosition(0, shootingOrigin.position);
        line.SetPosition(1, shootingOrigin.position + direction * 10);
        line.SetWidth(0.005f, 0.005f);

        StartCoroutine(DestroyGameObjectAfterSeconds(lineRend, 0.5f));

        //Look for target
        NetworkTarget target = hit.collider.GetComponent<NetworkTarget>();
        if (target)
        {
            target.SetPlayerScore(playerScore);
            target.TargetHitServer();
        }
    }

    IEnumerator DestroyGameObjectAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    IEnumerator FireMultiple()
    {
        int bulletsFired = 0;
        timeSinceLastBurstShot = gun.burstRechamberTime;

        while (bulletsFired != gun.burstSize)
        {
            if(timeSinceLastBurstShot >= gun.burstRechamberTime)
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

    public void Reload()
    {
        OnReloadEvent.Invoke();
        currentClipCount = 0;
        StartCoroutine(ReloadLoop());
    }

    IEnumerator ReloadLoop()
    {
        yield return new WaitForSeconds(gun.reloadTime);
        currentClipCount = gun.clipSize;
    }
}

