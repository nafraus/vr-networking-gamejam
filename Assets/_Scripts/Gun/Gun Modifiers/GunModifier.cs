using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunModifier", fileName = "ScriptableObject/GunModifier", order = 0)]
public class GunModifier : ScriptableObject
{
    public List<GunMods> GunMods;
    public int PointsCost;
    public float AllowedUpgradeOccurances = 1;
}


[System.Serializable]
public struct GunMods
{
    public enum ModifierType
    {
        ReduceSpreadValue,
        IncreaseBulletRadius,
        AddBurstCount,
        AddClipSize,
        ReduceTriggerThreshhold,
        ReduceFireRate,
        FullLaser
    }

    public ModifierType modifierType;
    public bool IsReplacing;
    public float Value;
}
