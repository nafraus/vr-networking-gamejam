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
        GameObject.FindGameObjectWithTag("PlayerScore");
    }
    void TryActivateGunModifier()
    {
        if(score.Score >= modifier.PointsCost) score.AddScoreServerRpc(-modifier.PointsCost);
    }
}
