using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerScore : NetworkBehaviour
{
    public int Score { get; private set; }

    [ServerRpc]
    public void AddScoreServerRpc(int i)
    {
        Score += i;
    }
}
