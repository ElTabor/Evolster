using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    private float _lastLightEnemySpawned;
    [SerializeField] private float lightEnemySpawnCooldown;
    private float _lastHeavyEnemySpawned;
    [SerializeField] private float heavyEnemySpawnCooldown;
    private float _lastRangeEnemySpawned;
    [SerializeField] private float rangeEnemySpawnCooldown;
    [SerializeField] private GameObject[] spawns;

    private void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    private void Update()
    {
        if (SceneManager.instance.scene != "Endless" && SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby")
            if (spawns != null && RoundsManager.instance.round < 7)
                spawns = GameObject.FindGameObjectsWithTag("Spawn");

        if (SceneManager.instance.scene == "Endless")
            spawns = GameObject.FindGameObjectsWithTag("Spawn");

        if (Time.time > _lastLightEnemySpawned + lightEnemySpawnCooldown)
        {
            Debug.Log("light enemy aaa");
            ChooseSpawn("light enemy");
            _lastLightEnemySpawned = Time.time;
        }
        if (Time.time > _lastRangeEnemySpawned + rangeEnemySpawnCooldown)
        {
            Debug.Log("range enemy aaa");
            ChooseSpawn("range enemy");
            _lastRangeEnemySpawned = Time.time;
        }
        if (Time.time > _lastHeavyEnemySpawned + heavyEnemySpawnCooldown)
        {
            Debug.Log("heavy enemy aaa");
            ChooseSpawn("heavy enemy");
            _lastHeavyEnemySpawned = Time.time;
        }
    }

    private void ChooseSpawn(string enemyToSpawn)
    {
        int r = Random.Range(0, spawns.Length);
        spawns[r].GetComponent<EnemySpawn>().SpawnEnemy(true, enemyToSpawn);
    }
}
