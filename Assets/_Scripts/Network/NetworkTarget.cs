using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

public class NetworkTarget : NetworkBehaviour
{
    [SerializeField] private int score;
    [ReadOnly] private PlayerScore playerScore;

    private BoxCollider bColl;
    [SerializeField]private MeshRenderer mr;

    private void Start()
    {
        bColl = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// Despawns the target and adds score for the specified player.  
    /// </summary>
    public void TargetHitServer()
    {
        // Despawn
        DespawnServerRpc();
        
        // Disable collider and renderer
        bColl.enabled = false;
        mr.enabled = false;

        // Add score to score manager for the provided player
        playerScore.AddScoreServerRpc(score);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnServerRpc()
    {
        // Destroy self
        NetworkObject.Despawn();
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
