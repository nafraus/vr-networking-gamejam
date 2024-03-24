using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShootableButton> buttons = new List<ShootableButton>();
    [SerializeField] private GunsSO gun;

    [Button]
    public void Enable()
    {
        foreach (var button in buttons) 
        { 
            button.Enable();
            gun.SetActiveGun(GunsSO.SetGunType.UI);
        }
    }

    [Button]
    public void Disable()
    {
        foreach (var button in buttons)
        {
            button.Disable();
            gun.SetActiveGun(GunsSO.SetGunType.CurrentGameGun);
        }
    }

    [Button]
    public void ResetShop()
    {
        foreach (var button in buttons)
        {
            button.ResetButton();
        }
    }
}
