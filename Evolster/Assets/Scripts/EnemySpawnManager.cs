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
        foreach (EnemySpawn spawn in enemySpawnPoints)
        {
            spawn.enemiesToSpawn = RoundsManager.instance.round * 2;
            StartCoroutine(spawn.SpawnEnemy());
        }
    }
}
