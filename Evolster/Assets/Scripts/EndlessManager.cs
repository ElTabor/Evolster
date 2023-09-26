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
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    private void Update()
    {
        if (Time.time > lastLightEnemySpawned + lightEnemySpawnCooldown)
        {
            ChooseSpawn("light enemy");
            lastLightEnemySpawned = Time.time;
        }
        if (Time.time > lastHeavyEnemySpawned + heavyEnemySpawnCooldown)
        {
            ChooseSpawn("heavy enemy");
            lastHeavyEnemySpawned = Time.time;
        }
        if (Time.time > lastRangeEnemySpawned + rangeEnemySpawnCooldown)
        {
            ChooseSpawn("range enemy");
            lastRangeEnemySpawned = Time.time;
        }
    }

    void ChooseSpawn(string enemyToSpawn)
    {
        int r = Random.Range(0, spawns.Length);
        spawns[r].GetComponent<EnemySpawn>().SpawnEnemy(true, enemyToSpawn);
        Debug.Log("EnemyEnqueued");
    }
}
