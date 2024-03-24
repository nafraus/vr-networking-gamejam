using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using BezierSolution;
using Unity.Netcode;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class NetworkGameManager : NetworkBehaviour
{
    private enum GameState { RideStart, Shopping, Dueling, RideEnd }
    [Header("General")]
    private GameState gameState = GameState.RideStart;
    [SerializeField] private float shoppingTime = 10f;
    [SerializeField] private int maxLaps = 3;
    private int currentLap;

    [Header("Tracks/Paths")]
    private BezierSpline[] trackASegments;
    private BezierSpline[] trackBSegments;
    
    private Dictionary<ulong, NetworkPlayer> players;
    private Dictionary<ulong, PlayerScore> playerScores;
    private BezierWalker[] splineWalkers;

    [Header("Audio")]
    [SerializeField] private UnityEvent CountdownAudio;

    public override void OnNetworkSpawn()
    {
        splineWalkers = FindObjectsOfType<BezierWalker>();
        if (splineWalkers.Length > 0)
        {
            ((BezierWalkerWithSpeed)splineWalkers[0]).spline = trackASegments[0];
        }

        StartCoroutine(nameof(StartGameLoop));
    }

    public void AddPlayer(ulong id, NetworkPlayer np)
    {
        players.Add(id, np);
        playerScores.Add(id, np.GetComponent<PlayerScore>()); // THIS MIGHT NOT GRAB THE REFERENCE CORRECTLY
    }
    
    private IEnumerator StartGameLoop()
    {
        yield return new WaitUntil(() => NetworkManager.IsConnectedClient);
        StartCoroutine(nameof(GameLoop));
    }

    private IEnumerator GameLoop()
    {
        switch (gameState)
        {
            case GameState.RideStart:
            {
                // Wait for ready up from both players
                if (true)
                {
                    // Do countdown launch sequence
                    for (int i = 0; i < 3; i++)
                    {
                        yield return new WaitForSeconds(1);

                        // Play audio clip
                        CountdownAudio?.Invoke();
                    }

                    // Start both player's rides
                    //==================================================================================================

                    // Change gameState
                    gameState = GameState.Dueling;
                }

                break;
            }

            case GameState.Shopping:
            {
                Debug.Log("SHOPPING");
                
                // Enable shopping
                //======================================================================================================

                // Wait for shopping to be over
                yield return new WaitForSeconds(shoppingTime);

                // Disable shopping
                //======================================================================================================

                // Change gameState
                // If at last spline segment of track
                if (splineWalkers[0].Spline == trackASegments[^1])
                {
                    // If end of ride
                    if (currentLap >= maxLaps)
                    {
                        gameState = GameState.RideEnd;
                    }
                }
                else // Else start the next dueling segment
                {
                    gameState = GameState.Dueling;
                }
                
                break;
            }

            case GameState.Dueling:
            {
                // DO NOTHING
                // STATE CHANGE IS HANDLED USING SPLINE EVENTS CALLING EnableShopping
                break;
            }

            case GameState.RideEnd:
            {
                // Transition to post match scene
                    // NEW SCENE SHOULD:
                    // Display scores
                    // Start timer
                    // Enable UI to allow player to quit game and return to lobby
                
                Debug.Log("RIDE END");
                break;
            }
        }

        // If ride is not over, call next game loop
        if (gameState != GameState.RideEnd)
        {
            StartCoroutine(nameof(GameLoop));
        }
    }

    /// <summary>
    /// Called by the bezier solution to enable shopping and swap the gameState to Shopping
    /// </summary>
    public void EnableShopping()
    {
        Debug.Log("GOING SHOPPING");
        gameState = GameState.Shopping;
    }
}