using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void ApplyBuff(float buff, float buffTime)
    {
        PlayerController.instance.currentDamage += buff;
        StartCoroutine(StartCountdown(buff, buffTime));
    }

    public void RemoveBuff(float buff) => PlayerController.instance.currentDamage -=  buff;

    IEnumerator StartCountdown(float buff, float buffTime)
    {
        yield return new WaitForSeconds(buffTime);
        RemoveBuff(buff);
    }
}
