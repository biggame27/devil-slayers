using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int score;

    public PlayerData (PlayerController player)
    {
        score = Mathf.Max(player.GetScore().GetScore(), score);
    }
}
