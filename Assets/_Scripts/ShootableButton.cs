using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootableButton : MonoBehaviour
{
    [SerializeField] private ApplyGunModifier applyGunModifier;
    [SerializeField] private UnityEvent applyModifierFailed;
    [SerializeField] private UnityEvent applyModifierSuccess;

    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Renderer rend;

    private int allowedModifierOccurances => applyGunModifier.GetModifier().AllowedUpgradeOccurances;
    private int modificationOccurances = 0;
    private bool isEnabled = true;

    private void Start()
    {
        Enable();
    }

    public void Activate()
    {
        Debug.Log("Button Shot");

        if (!isEnabled) return;

        bool isSuccessful = applyGunModifier.TryActivateGunModifier();

        if (isSuccessful)
        {
            modificationOccurances++;
            applyModifierSuccess.Invoke();

            if (modificationOccurances == allowedModifierOccurances) Disable();
        }
        else
        {
            applyModifierFailed.Invoke();
        }
    }

    public void Disable()
    {
        isEnabled = false;
        rend.materials[0].color = disabledColor;
    }

    public void Enable()
    {
        isEnabled = true;
        rend.materials[0].color = enabledColor;
    }

    public void ResetButton()
    {
        modificationOccurances = 0;
        Enable();
    }
}

