
using System;
using BezierSolution;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking.Transport;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private Renderer[] rends;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        if (IsOwner)
        {
            foreach (Renderer rend in rends)
            {
                rend.enabled = false;
            }

            // Set correct spline based on hosting or joining
            BezierWalkerWithSpeed bwws = GetComponent<BezierWalkerWithSpeed>();
            bwws.spline = IsHost ? 
                FindObjectOfType<NetworkGameManager>().GetTrackAStart() : 
                FindObjectOfType<NetworkGameManager>().GetTrackBStart();
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            root.position = VRRigReferences.Singleton.root.position;
            root.rotation = VRRigReferences.Singleton.root.rotation;
        
            head.position = VRRigReferences.Singleton.head.position;
            head.rotation = VRRigReferences.Singleton.head.rotation;
        
            leftHand.position = VRRigReferences.Singleton.leftHand.position;
            leftHand.rotation = VRRigReferences.Singleton.leftHand.rotation;
        
            rightHand.position = VRRigReferences.Singleton.rightHand.position;
            rightHand.rotation = VRRigReferences.Singleton.rightHand.rotation;
        }
    }
}
