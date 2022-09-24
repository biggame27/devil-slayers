using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    private float distance;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public SwordAttack swordAttack;
    public GameObject enemySpawner;
    public Score score;

    public float minX, maxX, minY, maxY;
    public float health;
    private float maxHealth;
    public int worth = 1;
    private float waitTime = 3f;
    float patrolMoveSpeed = 0.3f;
    float followMoveSpeed = 0.6f;
    public Vector2 moveSpot;
    public HealthBar healthBar;

    bool following = false;
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
        maxHealth = health;
        enemySpawner = GameObject.Find("EnemySpawner");
        player = GameObject.Find("Player");
        score = GameObject.Find("Score").transform.GetChild(0).gameObject.GetComponent<Score>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpot = new Vector2(transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY,maxY));
        

        CheckBorders();
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            FollowPlayer();
            Patrol();
        }
    }

    void FollowPlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if(distance < .14 && Mathf.Abs(direction.y) < .04)
        {
            DoDelayAction(1f);
        }
        else if(distance < .78)
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
        animator.SetTrigger("Defeated");
        LockMovement();
    }

    public void RemoveEnemy()
    {
        enemySpawner.GetComponent<EnemySpawner>().currentMobs -= 1;
        score.AddScore(worth);
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
