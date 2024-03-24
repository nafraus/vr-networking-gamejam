using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScore : MonoBehaviour
{
    PlayerScore playerScore;
    [SerializeField] private TMP_Text text;
    
    public void DisplayEndScore()
    {
        text.text = playerScore.Score.ToString();
    }
}
