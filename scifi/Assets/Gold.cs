using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public Text goldText;
    int gold = 0;

    public void AddGold(int change)
    {
        gold += change;
        SetGold();
    }

    public void SubtractGold(int change)
    {
        gold -= change;
        SetGold();
    }

    public void SetGold()
    {
        goldText.text = gold.ToString("");
    }
}
