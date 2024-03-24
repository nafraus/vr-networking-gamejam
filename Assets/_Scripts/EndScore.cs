using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScore : MonoBehaviour
{
    [SerializeField] PlayerScore playerScore;
    [SerializeField] private TMP_Text text;


    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void DisplayEndScore()
    {
        text.text = playerScore.Score.ToString();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        DisplayEndScore();
    }

    public void EndGameButton()
    {
        Application.Quit();
    }
}
