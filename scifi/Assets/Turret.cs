using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform enemies;
    [SerializeField]
    private float range;
    Transform closestEnemy;

    void FixedUpdate()
    {
        
        foreach(Transform child in enemies)
        {
            if(child != null)
            {
                float dist = Vector3.Distance(transform.position, child.transform.position);
                if(dist < range)
                    closestEnemy = child;
            }
        }
        if(closestEnemy != null)
        {
            Vector3 dir = transform.position - closestEnemy.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
