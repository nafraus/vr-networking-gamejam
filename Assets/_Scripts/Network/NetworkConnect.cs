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
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.Serialization;

public class NetworkConnect : MonoBehaviour
{
    public int maxConnection = 2;
    public UnityTransport transport;
    public string joinCode;
    private Lobby currentLobby;
    /*
    [FormerlySerializedAs("inputField")] 
    public TMP_InputField inputField_TMP;
    [FormerlySerializedAs("hostJoinCode")] 
    public TMP_Text lobbyName_TMP;
    */
    private float heartBeat;
    
    private async void Awake()
    {
        // Set up unity services connection
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
        // Try to join a lobby or create one if none are found
        JoinOrCreate();
    }

    private void Update()
    {
        if (heartBeat > 15)
        {
            heartBeat -= 15;

            if (currentLobby != null && currentLobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }

        heartBeat += Time.deltaTime;
    }
    
    public async void Create()
    {
        // Allocate a new server
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        
        // Get allocated newly allocated server's join code
        joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        
        // Set host relay's data
        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        
        // Create lobby options
        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;
        lobbyOptions.Data = new Dictionary<string, DataObject>();
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, joinCode);
        lobbyOptions.Data.Add("JOIN_CODE", dataObject);
        
        // Set the current lobby
        currentLobby = await Lobbies.Instance.CreateLobbyAsync("LOBBY_NAME", 2, lobbyOptions); // ===================== 
        
        // Start the host
        NetworkManager.Singleton.StartHost();
        
        /*
        // == CODE JOIN SYSTEM's EXTRA BITS ==
        // Set join code UI text
        lobbyName_TMP.text = joinCode;
        */
    }
    
    public async void Join()
    {
        // Get first open lobby 
        currentLobby = await Lobbies.Instance.QuickJoinLobbyAsync();
        
        // Get join code from current lobby
        joinCode = currentLobby.Data["JOIN_CODE"].Value;
        
        // Get join allocation 
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        
        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        
        // Start the client
        NetworkManager.Singleton.StartClient();
    }

    public async void JoinOrCreate()
    {
        try
        {
            // Get first open lobby 
            currentLobby = await Lobbies.Instance.QuickJoinLobbyAsync();
        
            // Get join code from current lobby
            joinCode = currentLobby.Data["JOIN_CODE"].Value;
        
            // Get join allocation for lobby 
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        
            // Set client relay data to the allocation's host
            transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        
            // Start the client
            NetworkManager.Singleton.StartClient();
        }
        catch
        {
            Create();
        }
    }
}