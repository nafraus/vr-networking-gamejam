using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunSettings", menuName = "ScriptableObject/GunSettings", order = 0)]
public class GunSettingsSO : ScriptableObject
{
    //Firing
    [Header("Firing")]
    [SerializeField] public float fireThreshhold;
    [SerializeField] public int burstSize;
    [SerializeField] public float burstRechamberTime;
    [SerializeField] public float raycastRadius;

    //Recoil
    [Header("Recoil")]
    [SerializeField] public float recoilStrength;
    [SerializeField] public float recoilRecoveryRate;

    //Spread;
    [Header("Spread")]
    [SerializeField] public float spreadStrength;

    //Clip
    [Header("Clip")]
    [SerializeField] public int clipSize;
    [SerializeField] public float fireRateTime;
    [SerializeField] public float reloadTime;
}
