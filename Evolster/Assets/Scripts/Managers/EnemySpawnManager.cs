using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    [SerializeField] GameObject[] enemySpawnPoints;
    void Start()
    {
        instance = this;
        
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
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
