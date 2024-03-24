using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ApplyGunModifier : MonoBehaviour
{
    [SerializeField][Expandable] private GunModifier modifier;
    [SerializeField] private GunSettingsSO gunSettings;
    [SerializeField] private PlayerScore score;
    private void Awake()
    {
        if(!score) score = GameObject.FindGameObjectWithTag("PlayerScore").GetComponent<PlayerScore>();
    }

    [Button]
    public bool TryActivateGunModifier()
    {
        if (score.Score >= modifier.PointsCost)
        {
            score.AddScoreServerRpc(-modifier.PointsCost);
            Debug.Log("Applied Gun Modifier");
            gunSettings.ApplyGunModifier(modifier);
            return true;
        }
        else return false;
    }

    [Button]
    public void Clear()
    {
        gunSettings.ClearModifiers();
    }

    public GunModifier GetModifier()
    {
        return modifier;
    }

}
