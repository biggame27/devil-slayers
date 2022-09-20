using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Gold gold;
    int score = 0;

    public void AddScore(int change)
    {
        score += change;
        gold.AddGold(change);
        SetScore();
    }

    public void SetScore()
    {
        scoreText.text = "Score: " +score.ToString("");
    }
}
