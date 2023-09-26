using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform referencePoint;
    [SerializeField] GameObject lightEnemyPrefab;
    [SerializeField] GameObject heavyEnemyPrefab;
    [SerializeField] GameObject rangeEnemyPrefab;
    [SerializeField] GameObject bossPrefab;
    public int amountOfEnemiesToSpawn;
    public bool spawnBossNow;
    GameObject enemyToSpawn;
    [SerializeField] float spawnCooldown;
    private float lastSpawn;

    SpawnQueue spawnQueue;

    void Start()
    {
        spawnQueue = new SpawnQueue();
        spawnQueue.InitializeQueue();
        lastSpawn = Time.time;
    }

    private void Update()
    {
        if (!spawnQueue.EmptyQueue()) Spawn();
    }

    private void Spawn()
    {
        if (Time.time > lastSpawn + spawnCooldown)
        {
            Instantiate(spawnQueue.First(), referencePoint.position, Quaternion.identity);
            spawnQueue.Dequeue();
            lastSpawn = Time.time;
            Debug.Log("Enemy spawned");
        }
    }

    public void SpawnEnemy(bool isEndless, string newEnemy)
    {
        Debug.Log("Spawn enemy");
        GameObject enemy = SelectEnemy(isEndless, newEnemy);
        if (isEndless)
        {
            spawnQueue.Enqueue(enemy);
        }
        else
        {
            for (int i = 0; i < amountOfEnemiesToSpawn; i++)
            {
                spawnQueue.Enqueue(enemy);
                spawnBossNow = false;
            }
        }
    }

    GameObject SelectEnemy(bool isEndless, string newEnemy)
    {
        if(isEndless)
        {
            switch(newEnemy)
            {
                case "light enemy":
                    return lightEnemyPrefab;
                case "heavy enemy":
                    return heavyEnemyPrefab;
                case "range enemy":
                    return rangeEnemyPrefab;
                default:
                    Debug.Log("No enemy selected");
                    return null;
            }
        }
        else
        {
            if (spawnBossNow) return bossPrefab;
            else return lightEnemyPrefab;
        }
    }
}
