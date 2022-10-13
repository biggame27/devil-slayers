using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public float health;
    public HealthBar healthBar;
    [SerializeField]
    private PauseManager pauseManager;
    [SerializeField]
    private PlayerController playerController;
    private float maxHealth;

    void Start()
    {
        maxHealth = health;
    }

    public float Health
    {
        set 
        {
            health = value;
            healthBar.SetHealth(value/maxHealth*100f);
            if(health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public void Defeated()
    {
        playerController.SavePlayer();
        pauseManager.Death();
    }

}
