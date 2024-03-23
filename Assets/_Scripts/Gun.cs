using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Threading;

public class Gun : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private InputActionProperty fireReference;
    [SerializeField] private float fireThreshhold;

    //Firing
    [Header("Firing")]
    [SerializeField] private int burstSize;
    [SerializeField] private float burstRechamberTime;
    public UnityEvent OnFireEvent;
    [SerializeField] private float raycastRadius;

    //Recoil
    [Header("Recoil")]
    [SerializeField] private float recoilStrength;
    [SerializeField] private float recoilRecoveryRate;

    //Spread;
    [Header("Spread")]
    [SerializeField] private float spreadStrength;

    //Clip
    [Header("Clip")]
    [SerializeField] private int clipSize;
    [SerializeField] private float fireRateTime;
    [SerializeField] private float reloadTime;
    #endregion

    //Effects Dictionary / List

    private int currentClipCount;
    private float timeSinceLastShot;
    private float timeSinceLastBurstShot;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float fireValue = fireReference.action.ReadValue<float>();
        if (fireValue < fireThreshhold) return;

        bool fireIsValid = ValidateFire();

        if (fireIsValid) DoFire();
        else DoFireFailed();

        
    }

    bool ValidateFire()
    {
        return true;
    }

    void DoFire()
    {
        //Expend an ammo
        currentClipCount--;

        //Fire, single or burst shot
        if (burstSize == 1) FireOnce();
        else FireMultiple();
    }

    void FireOnce()
    {

    }

    IEnumerator FireMultiple()
    {
        int bulletsFired = 0;
        timeSinceLastBurstShot = 0;

        while (bulletsFired != burstSize)
        {
            timeSinceLastBurstShot += Time.fixedDeltaTime;
            if(timeSinceLastBurstShot >= burstRechamberTime)
            {
                void FireOnce()
                {

                }
                timeSinceLastBurstShot = 0;
            }

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
        yield return new WaitForSeconds(reloadTime);
        currentClipCount = clipSize;
    }
}

