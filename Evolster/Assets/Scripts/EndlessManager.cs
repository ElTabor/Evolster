using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    [SerializeField] GameObject lightEnemyPrefab;
    private float lastLightEnemySpawned;
    [SerializeField] float lightEnemySpawnCooldown;
    [SerializeField] GameObject heavyEnemyPrefab;
    private float lastHeavyEnemySpawned;
    [SerializeField] float heavyEnemySpawnCooldown;
    [SerializeField] GameObject rangeEnemyPrefab;
    private float lastRangeEnemySpawned;
    [SerializeField] float rangeEnemySpawnCooldown;
    [SerializeField] private GameObject[] spawns;

    private void Start()
    {
        //spawns = GameObject.FindWithTag("Spawn").GetComponent<EnemySpawn>();
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    private void Update()
    {
        if (Time.time > lastLightEnemySpawned + lightEnemySpawnCooldown)
        {
            SpawnEnemy(lightEnemyPrefab);
            lastLightEnemySpawned = Time.time;
        }
        if (Time.time > lastHeavyEnemySpawned + heavyEnemySpawnCooldown)
        {
            SpawnEnemy(heavyEnemyPrefab);
            lastHeavyEnemySpawned = Time.time;

        }
        if (Time.time > lastRangeEnemySpawned + rangeEnemySpawnCooldown)
        {
            SpawnEnemy(rangeEnemyPrefab);
            lastRangeEnemySpawned = Time.time;

        }
    }

    void SpawnEnemy(GameObject enemyToSpawn)
    {
        int r = Random.Range(0, spawns.Length - 1);
        StartCoroutine(spawns[r].GetComponent<EnemySpawn>().SpawnEnemy(enemyToSpawn));
    }
}
