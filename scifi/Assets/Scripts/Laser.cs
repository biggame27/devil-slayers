using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private string tagCheck;
    
    Animator animator;
    Collider2D myCollider;
    private float damage;

    void Start()
    {
        damage = transform.parent.gameObject.GetComponent<Turret>().damage;
        spriteRenderer.enabled = false;
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        GetComponent<Collider2D>().enabled = false;
    }

    public void Fire()
    {
        spriteRenderer.enabled = true;
        myCollider.enabled = true;
    }

    public void StopFire()
    {
        spriteRenderer.enabled = false;
        myCollider.enabled = false;
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
            }
        }
    }
}
