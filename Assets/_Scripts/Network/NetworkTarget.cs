using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

public class NetworkTarget : NetworkBehaviour
{
    [SerializeField] private int score;
    [ReadOnly] private NetworkPlayerScore netPlayScore;

    /// <summary>
    /// Despawns the target and adds score for the specified player 
    /// </summary>
    [ServerRpc]
    public void TargetHitServerRpc()
    {
        // Destroy self
        NetworkObject.Despawn();

        // Add score to score manager for the provided player
        netPlayScore.AddScoreServerRpc(score);
    }

    /// <summary>
    /// To be called on client to tell target which player earns score when hit
    /// </summary>
    /// <param name="nps"></param>
    public void SetNetworkPlayerScore(NetworkPlayerScore nps)
    {
        netPlayScore = nps;
    }
}
