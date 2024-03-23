using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

public class NetworkTarget : NetworkBehaviour
{
    [SerializeField] private int score;
    [ReadOnly] private PlayerScore playerScore;

    /// <summary>
    /// Despawns the target and adds score for the specified player.  
    /// </summary>
    public void TargetHitServer()
    {
        // Destroy self
        NetworkObject.Despawn();

        // Add score to score manager for the provided player
        playerScore.AddScoreServerRpc(score);
    }

    /// <summary>
    /// To be called on client to tell target which player earns score when hit.
    /// </summary>
    /// <param name="ps"> Specified PlayerScore. </param>
    public void SetPlayerScore(PlayerScore ps)
    {
        playerScore = ps;
    }
}
