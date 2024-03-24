using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using Unity.Netcode;
using UnityEngine;

public class SplineAssignment : MonoBehaviour
{
    [SerializeField] private BezierWalkerWithSpeed bwws;
    private void Awake()
    {
        // Set correct spline based on hosting or joining
        bwws.spline = NetworkManager.Singleton.IsHost ? 
            FindObjectOfType<NetworkGameManager>().GetTrackAStart() : 
            FindObjectOfType<NetworkGameManager>().GetTrackBStart();
        
        Debug.Log($"{bwws.spline.gameObject.name}");
    }
}
