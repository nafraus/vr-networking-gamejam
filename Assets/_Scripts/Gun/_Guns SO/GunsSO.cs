using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Guns References", fileName ="GunsReferences", order =1)]
public class GunsSO : ScriptableObject
{
    /// <summary>
    /// Default Game Gun settings. Used at the start of the game
    /// </summary>
    public GunSettingsSO defaultGun;
    /// <summary>
    /// Gun settings for using UI.
    /// </summary>
    public GunSettingsSO uiGun;
    /// <summary>
    /// Current Gun used for gameplay- but not active. For future use, can be swapped out for new guns. See active gun for the gun data that is actually used in game
    /// </summary>
    public GunSettingsSO currentGameGun;
    /// <summary>
    /// Active and enabled gun. This data is used for firing the gun
    /// </summary>
    public GunSettingsSO activeGun;

    public void SetActiveGun(SetGunType gunType)
    {
        switch (gunType)
        {
            case SetGunType.Default: 
                activeGun = defaultGun; 
                break;
            case SetGunType.UI:
                activeGun = uiGun;
                break;
            case SetGunType.CurrentGameGun:
                activeGun = currentGameGun;
                break;
        }
    }

    /// <summary>
    /// Used as an override
    /// </summary>
    /// <param name="gun"></param>
    public void SetActiveGun(GunSettingsSO gun)
    {
        activeGun = gun;
    }

    public void SetCurrentGun(SetGunType gunType)
    {
        switch (gunType)
        {
            case SetGunType.Default:
                currentGameGun = defaultGun;
                break;
            case SetGunType.UI:
                currentGameGun = uiGun;
                break;
            case SetGunType.CurrentGameGun:
                currentGameGun = currentGameGun;
                break;
        }
    }

    public void SetCurrentGun(GunSettingsSO gun)
    {
        currentGameGun = gun;
    }

    public enum SetGunType
    {
        Default,
        UI,
        CurrentGameGun
    }
}
