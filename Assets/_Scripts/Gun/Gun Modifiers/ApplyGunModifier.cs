using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ApplyGunModifier : MonoBehaviour
{
    [SerializeField][Expandable] private GunModifier modifier;
    [SerializeField] private GunSettingsSO gunSettings;
    PlayerScore score;
    private void Awake()
    {
        score = GameObject.FindGameObjectWithTag("PlayerScore").GetComponent<PlayerScore>();
    }

    [Button]
    public bool TryActivateGunModifier()
    {
        if (score.Score >= modifier.PointsCost)
        {
            score.AddScoreServerRpc(-modifier.PointsCost);
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
}
