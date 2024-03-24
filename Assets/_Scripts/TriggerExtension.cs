using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class TriggerExtension : MonoBehaviour
{
    public UnityEvent triggerEnter;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;

    private BoxCollider bColl;

    private void Awake()
    {
        bColl = GetComponent<BoxCollider>();
        
        if (!bColl.isTrigger)
        {
            bColl.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnter?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        triggerStay?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExit?.Invoke();
    }
}
