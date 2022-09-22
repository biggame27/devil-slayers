using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Turret turret;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        spriteRenderer.enabled = false;
        animator = GetComponent<Animator>();
    }

    public void Fire()
    {
        spriteRenderer.enabled = true;

    }

    public void StopFire()
    {
        spriteRenderer.enabled = false;
    }
    
    public void OnTriggerEnter2D()
    {

    }
}
