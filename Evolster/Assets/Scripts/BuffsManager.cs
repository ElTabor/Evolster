using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    public static BuffsManager instance;
    [SerializeField] GameObject buff;
    BuffsStack stack;
    Vector2 buffSpawn;
    float buffAplicationTime;
    float buffTime;

    void Start()
    {
        instance = this;
        stack = new BuffsStack();
        stack.InitializeStack();
    }

    private void Update()
    {
        UIManager.instance.buffBar.SetActive(PlayerController.instance.isBuffed);
        if (PlayerController.instance.isBuffed) UIManager.instance.UpdateBuff(buffAplicationTime, buffTime);    
    }

    public void SetSpawnPosition(Vector2 newPosition)
    {
        Debug.Log(newPosition);
        buffSpawn = newPosition;
        stack.Push(buff);
        TrySpawnBuff();
    }

    void TrySpawnBuff()
    {
        if(!PlayerController.instance.isBuffed && GameObject.FindGameObjectsWithTag("Buff").Count() == 0)
        {
            GameObject newBuff = Instantiate(stack.Last(), buffSpawn, Quaternion.identity);
            stack.Pop();
        }
    }

    public void ApplyBuff(float buff, float buffTime)
    {
        Debug.Log("Aplicar buff");
        PlayerController.instance.currentDamage += buff;
        PlayerController.instance.isBuffed = true;
        buffAplicationTime = Time.time;
        this.buffTime = buffTime;
        StartCoroutine(StartCountdown(buff, buffTime));
    }

    public void RemoveBuff(float buff)
    {
        Debug.Log("Quitar buff");
        PlayerController.instance.currentDamage -=  buff;
        PlayerController.instance.isBuffed = false;
    }

    IEnumerator StartCountdown(float buff, float buffTime)
    {
        yield return new WaitForSeconds(buffTime);
        RemoveBuff(buff);
    }
}
