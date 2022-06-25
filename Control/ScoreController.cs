using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;

    public int Score {get; set;}

    public void AddPointsToScore(int points)
    {
        Score += points;
        scoreText.text = Score.ToString();
    }

    public void ResetScore()
    {
        Score = 0;
        scoreText.text = Score.ToString();
    }
}
