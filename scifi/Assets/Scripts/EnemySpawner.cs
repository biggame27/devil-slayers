using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject mudGuardPrefab;

    [SerializeField]
    private float mudGuardInterval = 7f;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int maxMobs = 5;

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
            currentMobs+=2;
            GameObject newEnemy = Instantiate(enemy, spawnPoints[Random.Range(0,15)].position, Quaternion.identity);
            GameObject newEnemy2 = Instantiate(enemy, spawnPoints[Random.Range(0,15)].position, Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
