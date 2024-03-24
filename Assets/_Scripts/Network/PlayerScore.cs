using System;
using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int Score { get => score.Value; private set => score = new NetworkVariable<int>(value); }
    [field: ReadOnly, SerializeField] private NetworkVariable<int> score;
    
    //[ServerRpc(RequireOwnership = false)]
    public void AddScoreServerRpc(int i) // DON'T KNOW ABOUT RPC-NESS?
    {
        Score += i;
    }
}
