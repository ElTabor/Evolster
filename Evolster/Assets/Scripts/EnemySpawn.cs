using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform referencePoint;
    [SerializeField] GameObject enemiePrefab;
    [SerializeField] int enemiesToSpawn;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

     public IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(enemiePrefab, referencePoint.position, Quaternion.identity);
        }
    }
}
