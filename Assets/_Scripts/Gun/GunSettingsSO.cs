using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


[CreateAssetMenu(fileName = "GunSettings", menuName = "ScriptableObject/GunSettings", order = 0)]
public class GunSettingsSO : ScriptableObject
{
    public bool isUIMode;
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

    private static Dictionary<ModifierType, List<GunMods>> modifiers = new Dictionary<ModifierType, List<GunMods>>();

    [HideInInspector] public Dictionary<ModifierType, List<GunMods>> Modifiers { get => modifiers; }

    public float GetModifierValueModifierType(ModifierType type)
    {
        if (isUIMode) return 0;
        if (!modifiers.ContainsKey(type)) return 0;

        float value = 0;
        List<GunMods> mods = modifiers[type];

        foreach (GunMods modifier in mods)
        {
            value += modifier.Value;
        }

        return value;
    }

    public void ApplyGunModifier(GunModifier mod)
    {
        foreach (GunMods modifier in mod.GunMods)
        {
            if (modifiers.ContainsKey(modifier.modifierType)) modifiers[modifier.modifierType].Add(modifier);
            else modifiers.Add(modifier.modifierType, new List<GunMods> { modifier });
        }
    }

    public void ClearModifiers()
    {
        modifiers.Clear();
    }
}
