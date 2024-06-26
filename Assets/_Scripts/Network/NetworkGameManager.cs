using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using BezierSolution;
using BezierSolution.Extras;
using Unity.Netcode;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using Unity.Services.Lobbies.Models;

public class NetworkGameManager : NetworkBehaviour
{
    private enum GameState { RideStart, Dueling, RideEnd }
    [Header("General")]
    private GameState gameState = GameState.RideStart;
    [SerializeField] private int maxLaps = 3;
    private int currentLap;

    [Header("Tracks/Paths")]
    [SerializeField] private BezierSpline[] trackASegments;
    [SerializeField] private BezierSpline[] trackBSegments;
    
    private Dictionary<ulong, NetworkObject> playerNObs;
    private Dictionary<ulong, PlayerScore> playerScores;
    public List<BezierWalkerWithSpeed> splineWalkers;

    [Header("Audio")]
    [SerializeField] private UnityEvent CountdownAudio;


    public override void OnNetworkSpawn()
    {
        // Get connected players
        // Debug.Log(NetworkManager.Singleton.ConnectedClients.Count);
        // for (int playerNum = 0; playerNum < NetworkManager.Singleton.ConnectedClients.Count; playerNum++)
        // {
        //     NetworkObject player = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
        //     playerNObs.Add(player.OwnerClientId, player);
        // }
        
        // Get player score components 
        // foreach (NetworkObject pNOb in playerNObs.Values)
        // {
        //     playerScores.Add(pNOb.OwnerClientId, pNOb.GetComponent<PlayerScore>());
        // }
        
        // Stall player spline walkers
        // foreach (NetworkObject playerNOb in playerNObs.Values)
        // {
        //     BezierWalkerStaller staller = playerNOb.GetComponent<BezierWalkerStaller>();
        //     staller.SetStallPoint(0);
        //     staller.DoStall = true;
        // }

        // Start game loop
        StartCoroutine(nameof(StartRide));
        gameState = GameState.RideStart;
    }

    // == DON'T KNOW IF THIS IS GETTING USED ANYMORE ==
    // private bool PlayersReady()
    // {
    //     // Short exit for not enough players
    //     if (playerNObs.Count < 2) return false;
    //     
    //     // For each player
    //     foreach (NetworkObject pNOb in playerNObs.Values)
    //     {
    //         // If not ready, return false
    //         if (!pNOb.GetComponent<ReadyUpSystem>().Ready)
    //         {
    //             return false;
    //         }
    //     }
    //
    //     // This line is only reached when all players are ready
    //     return true;
    // }

    private void Update()
    {
        switch (gameState)
        {
            default: break;
            
            case GameState.Dueling:
            {
                // If at last spline segment of track
                if (splineWalkers[0].Spline == trackASegments[^1])
                {
                    // If end of ride
                    if (currentLap >= maxLaps)
                    {
                        // Change gameState
                        gameState = GameState.RideEnd;
                    }
                }
                
                //Debug.Log("DUELING");
                break;
            }
        }
    }

    public void StartRide()
    {
        // Wait for ready up from both players
        if (true)//(NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            // Do countdown launch sequence
            // for (int i = 0; i < 3; i++)
            // {
            //     yield return new WaitForSeconds(1);
            //
            //     // Play audio clip
            //     CountdownAudio?.Invoke();
            // }

            // Start both player's rides
            foreach(BezierWalker walker in splineWalkers)
            {
                BezierWalkerStaller staller = walker.GetComponent<BezierWalkerStaller>();
                staller.DoStall = false;
            }

            // Change gameState
            gameState = GameState.Dueling;
        }
                
        Debug.Log("RIDE START");
    }

    /// <summary>
    /// Gets the starting segment for track A.
    /// </summary>
    /// <returns> The first BezierSpline in TracA. </returns>
    public BezierSpline GetTrackAStart()
    {
        return trackASegments[0];
    }
    
    /// <summary>
    /// Gets the starting segment for track A.
    /// </summary>
    /// <returns> The first BezierSpline in TracA. </returns>
    public BezierSpline GetTrackBStart()
    {
        return trackBSegments[0];
    }
}