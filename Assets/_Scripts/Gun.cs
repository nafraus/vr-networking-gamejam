using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Threading;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private XRDirectInteractor interactor;
    [SerializeField] private InputActionProperty fireReference;
    public UnityEvent OnFireEvent;
    [SerializeField] private GunSettingsSO gun;
    #endregion

    //Effects Dictionary / List

    private int currentClipCount;
    private float timeSinceLastShot;
    private float timeSinceLastBurstShot;
    private float isFireHeld;

    void Start()
    {
        currentClipCount = gun.clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.fixedDeltaTime;
        float fireValue = fireReference.action.ReadValue<float>();
        if (fireValue < gun.fireThreshhold) return;

        bool fireIsValid = ValidateFire();

        if (fireIsValid) DoFire();
        else DoFireFailed();
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
        interactor.SendHapticImpulse(0.5f, 0.1f);
        OnFireEvent.Invoke();
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

