using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class ReadyUpSystem : MonoBehaviour
{
    [field: ReadOnly, SerializeField] public bool Ready => numHandsReady == 2;

    [SerializeField] private TMP_Text text;

    private const string notReadyText = "PLACE HANDS IN ZONE TO READY";
    private const string readyText = "PLAYER READY";

    private BoxCollider bColl;
    private int numHandsReady;

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
        if (!other.CompareTag("Hand") )
        {
            return;
        }
        
        numHandsReady += 1;
        numHandsReady = Mathf.Clamp(numHandsReady, 0, 2);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Hand"))
        {
            return;
        }

        numHandsReady -= 1;
        numHandsReady = Mathf.Clamp(numHandsReady, 0, 2);
    }

    private void Update()
    {
        if(Ready) text.text = readyText;
        else text.text = notReadyText;
    }
}
