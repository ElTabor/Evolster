using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    List<EnemySpawn> enemySpawnPoints = new List<EnemySpawn>();
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < FindObjectsOfType<EnemySpawn>().Length; i++) enemySpawnPoints.Add(FindObjectsOfType<EnemySpawn>()[i]);
    }

    public void SetEnemiesToSpawn()
    { 
        if(RoundsManager.instance.round < 6)
        {
            foreach (EnemySpawn spawn in enemySpawnPoints)
            {
                spawn.amountOfEnemiesToSpawn = RoundsManager.instance.round * 2;
                StartCoroutine(spawn.SpawnEnemy());
            }
        }
        else
        {
            enemySpawnPoints[1].amountOfEnemiesToSpawn = 1;
            enemySpawnPoints[1].spawnBossNow = true;
            StartCoroutine(enemySpawnPoints[1].SpawnEnemy());
        }
    }
}
