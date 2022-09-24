using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public Text goldText;
    private int gold = 0;

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

    public int GetGold()
    {
        return gold;
    }

    public void SetGold()
    {
        goldText.text = gold.ToString("");
    }
}
