using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform referencePoint;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bossPrefab;
    public int amountOfEnemiesToSpawn;
    public bool spawnBossNow;
    GameObject enemyToSpawn;
    [SerializeField] float spawnCooldown;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

     public IEnumerator SpawnEnemy()
    {
        SelectEnemy();
        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnCooldown);
            Instantiate(enemyToSpawn, referencePoint.position, Quaternion.identity);
            spawnBossNow = false;
        }
    }

    void SelectEnemy()
    {
        if (spawnBossNow) enemyToSpawn = bossPrefab;
        else enemyToSpawn = enemyPrefab;
    }
}
