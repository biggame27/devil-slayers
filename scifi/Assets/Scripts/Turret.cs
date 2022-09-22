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

    void Start()
    {
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
    {;
        laser.Fire();
    }

    void StopLaserFire()
    {
        laser.StopFire();
    }
}
