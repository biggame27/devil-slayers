using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject bullet;

    void Start()
    {
        Fire();
    }

    IEnumerator Fire()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(bullet);
        }
    }

    
}
