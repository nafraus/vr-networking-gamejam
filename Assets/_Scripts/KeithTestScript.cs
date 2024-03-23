using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class KeithTestScript : MonoBehaviour
{
    public PlayerScore ps;
    public NetworkTarget nt;

    [Button]
    public void Test()
    {
        nt.SetPlayerScore(ps);
        nt.TargetHitServer();
    }
}
