using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootableButton : MonoBehaviour
{
    [SerializeField] private ApplyGunModifier applyGunModifier;
    [SerializeField] private UnityEvent applyModifierFailed;
    [SerializeField] private UnityEvent applyModifierSuccess;
    public void Activate()
    {
        bool isSuccessful = applyGunModifier.TryActivateGunModifier();

        if (isSuccessful)
        {
            applyModifierSuccess.Invoke();
        }
        else
        {
            applyModifierFailed.Invoke();
        }
    }
}
