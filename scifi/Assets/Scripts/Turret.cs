using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform _enemies;

    [SerializeField]
    private float waitTime;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Laser laser;

    [SerializeField]
    private float health;

    [SerializeField]
    private bool useLaser;

    [SerializeField]
    private Bullet bullet;

    private float maxHealth; 
    public HealthBar healthBar;
    public float damage;

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

    void Start()
    {
        maxHealth = health;
        _enemies = GameObject.Find("Enemies").transform;
        StartCoroutine(Fire());
    }

    void FixedUpdate()
    {   
        
        Transform closestEnemy = GetClosestEnemy(_enemies);
        if(closestEnemy != null)
        {
            //Debug.Log(closestEnemyPos.x + " " + closestEnemyPos.y);
            Vector3 dir = closestEnemy.position - transform.position;;
            transform.up = dir;
        }
    }
    Transform GetClosestEnemy(Transform enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    IEnumerator Fire()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            animator.SetTrigger("BeginFire");
        }
    }

    void LaserFire()
    {
        laser.Fire();
    }

    void StopLaserFire()
    {
        laser.StopFire();
    }

    void BulletFire()
    {
        Instantiate(bullet, transform.position, transform.rotation, transform);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    void DoubleBulletFire()
    {
        Vector2 leftBullet = new Vector2(transform.position.x-0.028f, transform.position.y);
        Instantiate(bullet, leftBullet, transform.rotation, transform);
        Vector2 rightBullet = new Vector2(transform.position.x+0.028f, transform.position.y);
        Instantiate(bullet, rightBullet, transform.rotation, transform);
        
    }

    public void Defeated()
    {
        Destroy(transform.parent.gameObject);
    }
}
