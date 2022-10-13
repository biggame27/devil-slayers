using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject mudGuardPrefab;

    [SerializeField]
    private float mudGuardInterval;

    [SerializeField]
    private int mudGuardAmount;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Transform storage;

    [SerializeField]
    private int maxMobs;

    public int currentMobs = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(mudGuardInterval, mudGuardPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if(currentMobs < maxMobs)
        {
            currentMobs+=mudGuardAmount;
            for(int i = 0; i < mudGuardAmount; i++)
            {
                GameObject newEnemy = Instantiate(enemy, spawnPoints[Random.Range(0,15)].position, Quaternion.identity, storage);
            }
            
        }
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
