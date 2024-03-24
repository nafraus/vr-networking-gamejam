using System;
using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int Score { get => score; private set => score = value; }
    [field: ReadOnly, SerializeField] private int score = 0;

    private void Awake()
    {
        ulong id = NetworkManager.Singleton.LocalClientId;
        GetComponent<NetworkGameManager>().AddPlayer(id, GetComponent<NetworkPlayer>());
    }
    
    public void AddScoreServerRpc(int i) // DON'T KNOW ABOUT RPC-NESS?
    {
        Score += i;
    }
}
