using NaughtyAttributes;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int Score { get => score; private set => score = value; }
    [field: ReadOnly, SerializeField] private int score = 0;
    
    public void AddScoreServerRpc(int i)
    {
        Score += i;
    }
}
