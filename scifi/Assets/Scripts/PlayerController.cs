using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public float health = 100f;
    Vector2 coords;

    public HealthBar healthBar;
    private PlayerInput playerInput;
    public GameObject turret;

    Ray ray;
    RaycastHit hit;

    public float Health
    {
        set 
        {
            health = value;
            healthBar.SetHealth(value);
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
    }

    void OnBuildLaser()
    {
        if(!building)
            building = true;
        else
            building = false;
    }
    /*
    void OnSwitchMap()
    {
        SwitchActionMap();
    }

    public void SwitchActionMap()
    {
        //enable only one
        //playerInput.SwitchCurrentActionMap("UI");
        //enable multiple
        playerInput.actions.FindActionMap("Player").Disable();
        playerInput.actions.FindActionMap("UI").Enable();
    }
    */

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
            animator.SetTrigger("Attack");
        else
        {
            if(!IsTouchingMouse())
            {
                Vector2 newCoords = coords;
                float timesX = Mathf.Round(coords.x/0.16f);
                float timesY = Mathf.Round(coords.y/0.16f);
                newCoords = new Vector2(0.16f * timesX, 0.16f * timesY);
                Instantiate(turret, newCoords, Quaternion.identity, turretStorage);
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
        bool check = false;
        foreach(Transform child in turretStorage)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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
