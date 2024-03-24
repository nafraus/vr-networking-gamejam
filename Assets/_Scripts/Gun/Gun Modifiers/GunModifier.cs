using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GunModifier", fileName = "GunModifier", order = 0)]
public class GunModifier : ScriptableObject
{
    public List<GunMods> GunMods;
    public int PointsCost;
    public int AllowedUpgradeOccurances = 1;
}


[System.Serializable]
public struct GunMods
{ 
    public ModifierType modifierType;
    public bool IsReplacing;
    public float Value;
}

public enum ModifierType
{
    ReduceSpreadValue,
    IncreaseBulletRadius,
    AddBurstCount,
    AddClipSize,
    ReduceTriggerThreshhold,
    ReduceFireRate,
    ReduceBurstRechamberTime,
    FullLaser
}

public static class ClearGunModifiers
{
    public static void Clear(GunSettingsSO gunSettings)
    {
        gunSettings.ClearModifiers();
    }
}
