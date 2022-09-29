using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3f;
    public string tagCheck;
    Collider2D swordCollider;
    Vector2 leftAttackOffset;

    private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        leftAttackOffset = transform.localPosition;
        StopAttack();
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(leftAttackOffset.x * -1, leftAttackOffset.y);
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = leftAttackOffset;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag ==tagCheck)
        {
            //Deal damage
            if(other.tag == "Enemy")
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if(enemy != null)
                    enemy.Health -= damage;
                
            }
            if(other.tag == "Player")
            {
                PlayerController enemy = other.GetComponent<PlayerController>();
                if(enemy != null)
                    enemy.Health -= damage;
                Turret turret = other.GetComponent<Turret>();
                if(turret != null)
                    turret.Health -= damage;
            }
        }
    }
}
