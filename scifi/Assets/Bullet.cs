using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private string tagCheck;
    [SerializeField]
    private float moveSpeed;
    private float damage;

    void FixedUpdate()
    {
        damage = transform.parent.gameObject.GetComponent<Turret>().damage;
        transform.position += transform.up*Time.deltaTime*moveSpeed;
        if(transform.position.x < -3 || transform.position.x > 3 || transform.position.y < -1.5 || transform.position.y > 1.5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag ==tagCheck)
        {  
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null)
                enemy.Health -= damage;
            Destroy(gameObject);
        }
    }
}
