using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Gold gold;
    int score = 0;
    int multiplier = 1;

    public void AddScore(int change)
    {
        if(PlayerPrefs.GetInt("EasyMode") == 0)
            multiplier = 2;
        score += change * multiplier;
        DisplayScore();
    }

    public void DisplayScore()
    {
        scoreText.text = "Score: " +score.ToString("");
    }

    public void SetScore(int _score)
    {
        score = _score;
    }

    public int GetScore()
    {
        return score;
    }
}
