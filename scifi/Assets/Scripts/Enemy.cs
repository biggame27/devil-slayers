using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public GameObject goldDrop;
    private float distance;

    Animator animator;
    Rigidbody2D rb;
    private Transform turrets;
    SpriteRenderer spriteRenderer;
    public SwordAttack swordAttack;
    public GameObject enemySpawner;
    public Score score;

    public float minX, maxX, minY, maxY;
    public float initHealth;
    private float health;
    private float maxHealth;
    public int worth = 1;
    [SerializeField]
    private float waitTime;
    public float patrolMoveSpeed = 0.2f;
    public float followMoveSpeed = 0.2f;
    public Vector2 moveSpot;
    public HealthBar healthBar;
    private Transform target;
    Transform goldStorage;
    [SerializeField]
    private float enemyScaling;
    private float time;

    bool following = false;
    bool defeated =false;
    bool canMove = true;
    
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

    private void Start()
    {
        goldStorage = GameObject.Find("Golds").transform;
        turrets = GameObject.Find("Turrets").transform;
        enemySpawner = GameObject.Find("EnemySpawner");
        player = GameObject.Find("Player");
        score = GameObject.Find("Score").transform.GetChild(0).gameObject.GetComponent<Score>();
        if(PlayerPrefs.GetInt("EasyMode") == 1)
        {
            //health = initHealth * ((score.GetScore()/150)*enemyScaling+1);
            health = Mathf.Pow(initHealth,((score.GetScore()/1000)*enemyScaling+1));
            followMoveSpeed = followMoveSpeed * (Mathf.Min(2.5f, 1+((score.GetScore()/1500)*enemyScaling)*0.01f));
        }
        else
        {
            //health = initHealth * ((score.GetScore()/300)*enemyScaling+1);
            health = Mathf.Pow(initHealth,((score.GetScore()/2000)*enemyScaling+1));
            followMoveSpeed = followMoveSpeed * (Mathf.Min(2.5f, 1+((score.GetScore()/3000)*enemyScaling)*0.01f));
        }
        maxHealth = health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpot = new Vector2(transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY,maxY));
        

        CheckBorders();
    }

    void FixedUpdate()
    {
        if(canMove && !player.GetComponent<PlayerController>().GetCanBuild())
        {
            time += Time.deltaTime;
            FollowPlayer();
            Patrol();
        }
    }

    Transform GetClosestEnemy(Transform enemies)
    {
        Transform tMin = player.transform;
        float minDist = Vector2.Distance(transform.position, player.transform.position);
        distance = minDist;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            if(t != null)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                    distance = minDist;
                }
            }
            
        }
        return tMin;
    }

    void FollowPlayer()
    {
        target = GetClosestEnemy(turrets);
        Vector2 direction = target.position - transform.position;

        if(distance < .14 && Mathf.Abs(direction.y) < .04)
        {
            DoDelayAction(1f);
        }
        else if(distance < .85 || time >= 100f)
        {
            direction.Normalize();
            rb.MovePosition(rb.position + direction*this.followMoveSpeed*Time.fixedDeltaTime);
            following = true;
            PlayWalkAnimation(true);
            moveSpot = player.transform.position;
            CheckDirection();
        }
        else
        {
            following = false;
        }
    }

    void Patrol()
    {
        if(following)
            return;
        //rb.MovePosition(rb.position + direction*this.moveSpeed*Time.fixedDeltaTime);
        transform.position = Vector2.MoveTowards(transform.position, moveSpot, patrolMoveSpeed * Time.deltaTime);
        //Debug.Log(moveSpot.position.x + " " + moveSpot.position.y);
        PlayWalkAnimation(true);
        CheckDirection();

        if(Vector2.Distance(transform.position, moveSpot) == 0)
        {
            PlayWalkAnimation(false);
            if(waitTime <= 0)
            {
                moveSpot = new Vector2(transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY,maxY));
                CheckBorders();
                waitTime = 3f;
            }else
                waitTime -= Time.deltaTime;
        }
        
    }

    void CheckBorders()
    {
        if(moveSpot.x > 3)
            moveSpot.x = 3;
        if(moveSpot.x < -3)
            moveSpot.x = -3;
        if(moveSpot.y < -1.5)
            moveSpot.y = -1.5f;
        if(moveSpot.y > 1.5)
            moveSpot.y = 1.5f;
    }

    void CheckDirection()
    {
        if(moveSpot.x-transform.position.x < 0)
            spriteRenderer.flipX = true;
        else if(moveSpot.x-transform.position.x > 0)
            spriteRenderer.flipX = false;
    }

    void PlayWalkAnimation(bool move)
    {
        animator.SetBool("IsMoving", move);
    }

    void SwordAttack()
    {
        LockMovement();
        if(spriteRenderer.flipX)
        {
            swordAttack.AttackRight();
        }
        else
        {
            swordAttack.AttackLeft();
        }
    }

    void EndSwordAttack()
    {
        swordAttack.StopAttack();
        UnlockMovement();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Defeated()
    {
        if(!defeated)
        {
            defeated = true;
            animator.SetTrigger("Defeated");
            Instantiate(goldDrop, transform.position, Quaternion.identity, goldStorage);
            LockMovement();
        }
        
    }

    public void RemoveEnemy()
    {
        enemySpawner.GetComponent<EnemySpawner>().currentMobs -= 1;
        score.AddScore(worth*100);
        Destroy(gameObject);
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    void DoDelayAction(float delayTime)
    {
        if(canMove)
            StartCoroutine(DelayAction(delayTime));
    }
    
    IEnumerator DelayAction(float delayTime)
    {
        LockMovement();
        yield return new WaitForSeconds(delayTime);
        animator.SetTrigger("Attack");
        
    }
}
