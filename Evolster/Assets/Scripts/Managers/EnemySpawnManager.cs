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
    [SerializeField] GameObject[] enemySpawnPoints;
    [SerializeField] GameObject enemySpawnPointPrefab;
    
    
    [SerializeField] private int amountOfSpawns;
    public float rangeX = 18f;
    public float rangeY = 9f;

    private void Start()
    {
        instance = this;
        
        //enemySpawnPointPrefab = GameObject.FindGameObjectWithTag("Spawn");
        
        RandomSpawnInstantiator();
    }

    private void RandomSpawnInstantiator()
    {
        for (int i = 0; i < amountOfSpawns; i++)
        {
            float posX = UnityEngine.Random.Range(-rangeX, rangeX);
            float posY = UnityEngine.Random.Range(-rangeY, rangeY);

            Vector3 randomPos = new Vector3(posX, posY, 0f);
            GameObject newSpawnPoint = Instantiate(enemySpawnPointPrefab, randomPos, Quaternion.identity);
            enemySpawnPoints.Append(newSpawnPoint);
        }
    }

    public void SetEnemiesToSpawn()
    { 
        if(RoundsManager.instance.round < 6)
        {
            foreach (GameObject spawn in enemySpawnPoints)
            {
                spawn.GetComponent<EnemySpawn>().amountOfEnemiesToSpawn = RoundsManager.instance.round * 2;
                spawn.GetComponent<EnemySpawn>().SpawnEnemy(false, null);
            }
        }
        else
        {
            enemySpawnPoints[1].GetComponent<EnemySpawn>().amountOfEnemiesToSpawn = 1;
            enemySpawnPoints[1].GetComponent<EnemySpawn>().spawnBossNow = true;
            enemySpawnPoints[1].GetComponent<EnemySpawn>().SpawnEnemy(false, null);
        }
    }
}
