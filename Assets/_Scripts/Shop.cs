using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShootableButton> buttons = new List<ShootableButton>();

    [Button]
    public void Enable()
    {
        foreach (var button in buttons) 
        { 
            button.Enable();    
        }
    }

    [Button]
    public void Disable()
    {
        foreach (var button in buttons)
        {
            button.Disable();
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
