using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using UnityEngine;

namespace BezierSolution.Extras
{
    [RequireComponent(typeof(BoxCollider))]
    public class BezierWalkerTransition : MonoBehaviour
    {
        [Tooltip("The BezierSpline this transition leads to.")]
        [SerializeField] private BezierSpline nextSpline;
    
        private BoxCollider bColl;

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
                bwws.spline = nextSpline;
                bwws.NormalizedT = 0;
            }
        }
    }
}
