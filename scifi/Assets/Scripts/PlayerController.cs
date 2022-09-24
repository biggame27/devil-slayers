using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public float health;
    private float maxHealth;
    Vector2 coords;

    public HealthBar healthBar;
    private PlayerInput playerInput;
    public GameObject turret;
    [SerializeField]
    private Gold gold;

    Ray ray;
    RaycastHit hit;

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

    public ContactFilter2D movementFilter;
    public Collider2D placingCheck;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    SpriteRenderer spriteRenderer;
    Animator animator;
    Transform turretStorage;

    public SwordAttack swordAttack;

    Vector2 movementInput;
    Rigidbody2D rb;

    bool canMove = true;
    bool building = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        turretStorage = GameObject.Find("Turrets").transform;
        maxHealth = health;
    }

    void OnBuildLaser()
    {
        if(!building)
        {
            placingCheck.enabled = true;
            building = true;
            LockMovement();
        }else
        {
            placingCheck.enabled = false;
            UnlockMovement();
            building = false;
        }
    }
    
    void FixedUpdate()
    {
        if(canMove && movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if(!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if(!success)
                    success = TryMove(new Vector2(0, movementInput.y));
            }
            animator.SetBool("IsMoving", true);
        } else {
            animator.SetBool("IsMoving", false);
        }
        if(movementInput.x > 0)
            spriteRenderer.flipX = true;
        else if(movementInput.x < 0)
            spriteRenderer.flipX = false;
    }

    bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if(count <= 1)
        {
            rb.MovePosition(rb.position + direction*moveSpeed*Time.fixedDeltaTime);
            return true;
        }
        return false;
    }
    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if(!building)
        {
            UnlockMovement();
            animator.SetTrigger("Attack");
        }
        else
        {
            if(!IsTouchingMouse() && gold.GetGold() >= 5)
            {
                Vector2 newCoords = coords;
                float timesX = Mathf.Round(coords.x/0.16f);
                float timesY = Mathf.Round(coords.y/0.16f);
                newCoords = new Vector2(0.16f * timesX, 0.16f * timesY);
                Instantiate(turret, newCoords, Quaternion.identity, turretStorage);
                gold.SubtractGold(5);
            }
        }
    }

    void OnLook(InputValue lookValue)
    {
        if(!building)
            return;
        coords = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    bool IsTouchingMouse()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        bool check = placingCheck.OverlapPoint(point);
        if(check) return true;
        foreach(Transform child in turretStorage)
        {
            check = child.gameObject.GetComponent<Collider2D>().OverlapPoint(point);
            if(check)
                return true;
        }
        return false;
        
    }

    void SwordAttack()
    {
        LockMovement();
        if(spriteRenderer.flipX)
            swordAttack.AttackRight();
        else
            swordAttack.AttackLeft();
    }

    void EndSwordAttack()
    {
        swordAttack.StopAttack();
        UnlockMovement();
    }

    void LockMovement()
    {
        canMove = false;
    }

    void UnlockMovement()
    {
        canMove = true;
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        LockMovement();
    }

    public Vector2 GetCoords()
    {
        return coords;
    }

    public bool GetCanBuild()
    {
        return building;
    }
}
