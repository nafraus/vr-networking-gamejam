using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using BezierSolution.Extras;
using Unity.Netcode;
using UnityEngine;

public class SplineAssignment : NetworkBehaviour
{
    [SerializeField] private BezierWalkerWithSpeed bwws;
    private void Awake()
    {
        // Set correct spline based on hosting or joining
        bwws.spline = NetworkManager.Singleton.IsHost ? 
            FindObjectOfType<NetworkGameManager>().GetTrackAStart() : 
            FindObjectOfType<NetworkGameManager>().GetTrackBStart();

        bwws.GetComponent<BezierWalkerStaller>().SetStallPoint(0);
        bwws.GetComponent<BezierWalkerStaller>().DoStall = true;

        AddToGMServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddToGMServerRpc()
    {
        Debug.Log("ADDING TO GM");
        FindObjectOfType<NetworkGameManager>().splineWalkers.Add(bwws);
    }
}
