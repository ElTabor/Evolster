using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform referencePoint;
    [SerializeField] private GameObject[] lightEnemyPrefab;
    [SerializeField] private GameObject[] heavyEnemyPrefab;
    [SerializeField] private GameObject[] rangeEnemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    public int amountOfEnemiesToSpawn;
    public bool spawnBossNow;
    [SerializeField] float spawnCooldown;
    private float _lastSpawn;

    [SerializeField] SpawnQueue _spawnQueue;

    private void Start()
    {
        _spawnQueue = new SpawnQueue();
        _spawnQueue.InitializeQueue();
        _lastSpawn = Time.time;
    }

    private void Update()
    {
        if (!_spawnQueue.EmptyQueue()) Spawn();
    }

    private void Spawn()
    {
        if (Time.time > _lastSpawn + spawnCooldown)
        {
            Debug.Log(_spawnQueue.First().name + " Spawner");
            Instantiate(_spawnQueue.First(), referencePoint.position, Quaternion.identity);
            _spawnQueue.Dequeue();
            _lastSpawn = Time.time;
        }
    }

    public void SpawnEnemy(bool isEndless, string newEnemy)
    {
        GameObject enemy = SelectEnemy(isEndless, newEnemy);
        if (isEndless)
        {
            _spawnQueue.Quicksort();
            _spawnQueue.Enqueue(enemy);
        }
        else
        {
            for (int i = 0; i < amountOfEnemiesToSpawn; i++)
            {
                _spawnQueue.Enqueue(enemy);
                spawnBossNow = false;
            }
        }
    }

    private GameObject SelectEnemy(bool isEndless, string newEnemy)
    {
        if(isEndless)
        {
            switch(newEnemy)
            {
                case "light enemy":
                    return lightEnemyPrefab[Random.Range(0, lightEnemyPrefab.Length)];
                case "heavy enemy":
                    return heavyEnemyPrefab[Random.Range(0, heavyEnemyPrefab.Length)];
                case "range enemy":
                    return rangeEnemyPrefab[Random.Range(0, rangeEnemyPrefab.Length)];
                default:
                    return lightEnemyPrefab[Random.Range(0, lightEnemyPrefab.Length)];
            }
        }
        else
        {
            if (spawnBossNow) return bossPrefab;
            else
            {
                int r = Random.Range(0, 101);
                if (r <= 65) return lightEnemyPrefab[GameManager.instance.currentLevel-1];
                else if (r <= 85) return rangeEnemyPrefab[GameManager.instance.currentLevel - 1];
                else return heavyEnemyPrefab[GameManager.instance.currentLevel - 1];
            } 
        }
    }
}
