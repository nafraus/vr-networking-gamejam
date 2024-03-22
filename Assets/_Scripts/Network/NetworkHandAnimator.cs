using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class NetworkHandAnimator : NetworkBehaviour
{
    [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference triggerReference;

    [SerializeField] private Animator animator;

    private void Update()
    {
        if (IsOwner)
        {
            float gripValue = gripReference.action.ReadValue<float>();
            animator.SetFloat("Grip", gripValue);
            float triggerValue = triggerReference.action.ReadValue<float>();
            animator.SetFloat("Trigger", triggerValue);
        }
    }
}
