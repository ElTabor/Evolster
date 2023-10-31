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
        }
    }

    public void SpawnEnemy(bool isEndless, string newEnemy)
    {
        GameObject enemy = SelectEnemy(isEndless, newEnemy);
        if (isEndless) spawnQueue.Enqueue(enemy);
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
            Debug.Log(newEnemy);
            switch(newEnemy)
            {
                case "light enemy":
                    return lightEnemyPrefab;
                case "heavy enemy":
                    return heavyEnemyPrefab;
                case "range enemy":
                    return rangeEnemyPrefab;
                default:
                    return lightEnemyPrefab;
            }
        }
        else
        {
            if (spawnBossNow) return bossPrefab;
            else
            {
                int r = Random.Range(0, 101);
                if (r <= 65) return lightEnemyPrefab;
                else if (r <= 85) return rangeEnemyPrefab;
                else return heavyEnemyPrefab;
            } 
        }
    }
}
