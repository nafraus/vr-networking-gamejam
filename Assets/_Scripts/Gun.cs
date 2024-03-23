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
    public UnityEvent OnFireEvent;
    [SerializeField] private GunSettingsSO gun;
    [SerializeField] private Transform shootingOrigin;
    #endregion

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
            bool fireIsValid = ValidateFire();

            if (fireIsValid) DoFire();
            else DoFireFailed();

            hasAlreadyFired = true;
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
        if (currentClipCount == 0) return false;

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
        else FireMultiple();
    }

    void FireOnce()
    {
        Debug.Log("Shots fired");
        interactor.SendHapticImpulse(0.85f, 0.1f);
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

        GameObject lineRend = new GameObject();
        LineRenderer line = lineRend.AddComponent<LineRenderer>();
        line.SetPosition(0, shootingOrigin.position);
        line.SetPosition(1, shootingOrigin.position + direction * 10);
        line.SetWidth(0.001f, 0.001f);

        StartCoroutine(DestroyGameObjectAfterSeconds(lineRend, 0.5f));
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
        //Code failed for if a fire does not happen
        interactor.SendHapticImpulse(0.05f, 0.1f);
    }

    public void Reload()
    {
        StartCoroutine(ReloadLoop());
    }

    IEnumerator ReloadLoop()
    {
        yield return new WaitForSeconds(gun.reloadTime);
        currentClipCount = gun.clipSize;
    }
}

