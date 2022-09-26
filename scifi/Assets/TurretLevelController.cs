using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLevelController : MonoBehaviour
{
    private int level = 1;
    
    public void LevelUp()
    {
        transform.Find("Level " + level).gameObject.SetActive(false);
        level++;
        transform.Find("Level " + level).gameObject.SetActive(true);
    }

    public void Sell()
    {
        Destroy(gameObject);
    }
}
