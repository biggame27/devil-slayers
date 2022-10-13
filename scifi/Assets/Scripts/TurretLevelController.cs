using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLevelController : MonoBehaviour
{
    private int level = 1;
    [SerializeField]
    private int[] upgradeCosts;
    [SerializeField]
    private int[] damages;
    private PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    public void LevelUp()
    {
        if(level-1 < upgradeCosts.Length && player.GetGold().GetGold() >= upgradeCosts[level-1])
        {
            transform.Find("Level " + level).gameObject.SetActive(false);
            player.GetGold().SubtractGold(upgradeCosts[level-1]);
            level++;
            transform.Find("Level " + level).gameObject.SetActive(true);
        }
        
    }

    public void Sell()
    {
        Destroy(transform.gameObject);
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCost()
    {
        if(level-1 == upgradeCosts.Length)
            return 69;
        return upgradeCosts[level-1];
    }

    public int GetDamage()
    {
        if(level-1 == damages.Length)
            return 69;
        return damages[level-1];
    }

    public int GetFutureDamage()
    {
        if(level >= damages.Length)
            return 69;
        return damages[level];
    }
}
