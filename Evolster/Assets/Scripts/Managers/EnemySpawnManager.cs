using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    [SerializeField] private List<GameObject> enemySpawnPoints = new List<GameObject>();
    [SerializeField] private GameObject enemySpawnPointPrefab;

    [SerializeField] private int amountOfSpawns;
    public float rangeX = 18f;
    public float rangeY = 9f;
    public float minDistanceBetweenSpawns = 5f;

    private void Start()
    {
        instance = this;
        RandomSpawnInstantiator();
    }

    private void RandomSpawnInstantiator()
    {
        int attempts = 0;
        while (enemySpawnPoints.Count < amountOfSpawns && attempts < amountOfSpawns * 10)
        {
            float posX = UnityEngine.Random.Range(-rangeX, rangeX);
            float posY = UnityEngine.Random.Range(-rangeY, rangeY);
            Vector3 randomPos = new Vector3(posX, posY, 0f);

            if (IsPositionValid(randomPos))
            {
                GameObject newSpawn = Instantiate(enemySpawnPointPrefab, randomPos, Quaternion.identity);
                enemySpawnPoints.Add(newSpawn);
            }
            attempts++;
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject spawnPoint in enemySpawnPoints)
        {
            if (Vector3.Distance(position, spawnPoint.transform.position) < minDistanceBetweenSpawns)
            {
                return false;
            }
        }
        return true;
    }

    public void SetEnemiesToSpawn()
    {
        if (RoundsManager.instance.round < 6)
        {
            foreach (GameObject spawn in enemySpawnPoints)
            {
                spawn.GetComponent<EnemySpawn>().amountOfEnemiesToSpawn = RoundsManager.instance.round * 2;
                spawn.GetComponent<EnemySpawn>().SpawnEnemy(false, null);
            }
        }
        else if (RoundsManager.instance.round == 6)
        {
            enemySpawnPoints[1].GetComponent<EnemySpawn>().amountOfEnemiesToSpawn = 1;
            enemySpawnPoints[1].GetComponent<EnemySpawn>().spawnBossNow = true;
            enemySpawnPoints[1].GetComponent<EnemySpawn>().SpawnEnemy(false, null);
        }
    }
}
