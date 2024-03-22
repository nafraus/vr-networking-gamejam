using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using TMPro;
using NaughtyAttributes;
using UnityEngine.Events;

public class NetworkConnect : MonoBehaviour
{
    public int maxConnection = 2;
    public UnityTransport transport;
    public string joinCode;
    public TMP_InputField inputField;
    public TMP_Text hostJoinCode;
    
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
    }

    [Button]
    public async void HostButtonPress()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        joinCode = newJoinCode;
        hostJoinCode.text = joinCode;
        
        //transport.SetRelayServerData();

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        
        
        NetworkManager.Singleton.StartHost();
    }
    
    [Button]
    
    public async void JoinButtonPress()
    {
        //testing to see if we can get joincode
        joinCode = inputField.text;
        Debug.Log(joinCode);
        
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        
        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        
        NetworkManager.Singleton.StartClient();
    }

    public void ServerButtonPress()
    {
        NetworkManager.Singleton.StartServer();
    }
}
