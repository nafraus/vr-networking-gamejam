using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BezierSolution.Extras
{
    [RequireComponent(typeof(BoxCollider))]
    public class BezierWalkerSpeedAdjustZone : MonoBehaviour
    {
        [field: SerializeField] public float TargetSpeed { get; private set; } = .25f;
        [SerializeField] private float accelerationRate = 6f;
        
        private BoxCollider bColl;
        private float initialSpeed;

        private void Awake()
        {
            // Get box collider
            bColl = GetComponent<BoxCollider>();
            
            // Confirm that box collider is a trigger
            if (!bColl.isTrigger)
            {
                bColl.isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            BezierWalkerWithSpeed bwws = other.GetComponent<BezierWalkerWithSpeed>();
            if (bwws != null)
            {
                initialSpeed  = bwws.speed;
                StartCoroutine(LerpSpeed(bwws, TargetSpeed));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BezierWalkerWithSpeed bwws = other.GetComponent<BezierWalkerWithSpeed>();
            if (bwws != null)
            {
                StartCoroutine(LerpSpeed(bwws, initialSpeed));
            }
        }

        private IEnumerator LerpSpeed(BezierWalkerWithSpeed bwws, float targetSpeed)
        {
            while (bwws.speed != targetSpeed)
            {
                bwws.speed = Mathf.Lerp(bwws.speed, targetSpeed, Time.deltaTime * accelerationRate);
                bwws.speed = Mathf.Clamp(bwws.speed, 0, targetSpeed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}