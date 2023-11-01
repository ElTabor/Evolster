using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    [SerializeField] GameObject lightEnemyPrefab;
    private float _lastLightEnemySpawned;
    [SerializeField] float lightEnemySpawnCooldown;
    [SerializeField] GameObject heavyEnemyPrefab;
    private float _lastHeavyEnemySpawned;
    [SerializeField] float heavyEnemySpawnCooldown;
    [SerializeField] GameObject rangeEnemyPrefab;
    private float _lastRangeEnemySpawned;
    [SerializeField] float rangeEnemySpawnCooldown;
    [SerializeField] private GameObject[] spawns;

    private void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    private void Update()
    {
        if (Time.time > _lastLightEnemySpawned + lightEnemySpawnCooldown)
        {
            ChooseSpawn("light enemy");
            _lastLightEnemySpawned = Time.time;
        }
        if (Time.time > _lastHeavyEnemySpawned + heavyEnemySpawnCooldown)
        {
            ChooseSpawn("heavy enemy");
            _lastHeavyEnemySpawned = Time.time;
        }
        if (Time.time > _lastRangeEnemySpawned + rangeEnemySpawnCooldown)
        {
            ChooseSpawn("range enemy");
            _lastRangeEnemySpawned = Time.time;
        }
    }

    void ChooseSpawn(string enemyToSpawn)
    {
        int r = Random.Range(0, spawns.Length);
        spawns[r].GetComponent<EnemySpawn>().SpawnEnemy(true, enemyToSpawn);
        Debug.Log("EnemyEnqueued");
    }
}
