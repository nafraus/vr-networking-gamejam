using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private PlayerScore score;
    [SerializeField] private float angleThreshhold = 10f;

    bool isInBounds => Vector3.Angle(Camera.main.transform.forward, transform.forward) <= angleThreshhold;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Angle(Camera.main.transform.forward, -transform.up));
        if (isInBounds) text.color = Color.white;
        else text.color = Color.clear;

        text.text = score.Score.ToString();
    }


}
