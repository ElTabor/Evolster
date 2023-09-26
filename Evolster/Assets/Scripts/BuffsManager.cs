using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    public static BuffsManager instance;
    [SerializeField] GameObject buff;
    BuffsStack stack;
    Vector2 buffSpawn;

    void Start()
    {
        instance = this;
        stack = new BuffsStack();
        stack.InitializeStack();
    }

    public void SetSpawnPosition(Vector2 newPosition)
    {
        Debug.Log(newPosition);
        buffSpawn = newPosition;
        stack.Push(buff);
        SpawnBuff();
    }

    void SpawnBuff()
    {
        GameObject newBuff = Instantiate(stack.Last(), buffSpawn, Quaternion.identity);
        stack.Pop();
    }
}
