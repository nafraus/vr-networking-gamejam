using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHapticsManager : MonoBehaviour
{
    public static VRHapticsManager instance;

    [SerializeField] private XRDirectInteractor leftInteractor;
    [SerializeField] private XRDirectInteractor rightInteractor;

    public enum DeviceSpecifier
    {
        left,
        right,
        both
    }

    private void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;
    }

    public void SendHapticImpulse(float amplitude, float duration, DeviceSpecifier type)
    {
        switch (type)
        {
            case DeviceSpecifier.left:
                leftInteractor.SendHapticImpulse(amplitude, duration);
                break;
            case DeviceSpecifier.right:
                rightInteractor.SendHapticImpulse(amplitude, duration);
                break;
            case DeviceSpecifier.both:
                leftInteractor.SendHapticImpulse(amplitude, duration);
                rightInteractor.SendHapticImpulse(amplitude, duration);
                break;
        }
    }
}


