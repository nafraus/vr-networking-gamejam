using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using UnityEngine;

namespace BezierSolution.Extras
{
    public class BezierWalkerStaller : MonoBehaviour
    {
        [field: SerializeField] public bool DoStall { get; set; } = false;
    
        private BezierWalkerWithSpeed walker;
        private float stallValue = 2;

        private void Awake()
        {
            walker = GetComponent<BezierWalkerWithSpeed>();
        }

        // Update is called once per frame
        void Update()
        {
            if (DoStall)
            {
                if (stallValue > 1)
                {
                    stallValue = walker.NormalizedT;
                }
                walker.NormalizedT = stallValue;
            }
        }

        public void SetStallPoint(float tValue = 2)
        {
            // Faster than float comparison
            stallValue = tValue > 1 ? 
                walker.NormalizedT : 
                tValue;
        }
    }
}