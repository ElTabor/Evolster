using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    public static BuffsManager instance;
    [SerializeField] private GameObject buff;
    private BuffsStack _stack;
    private Vector2 _buffSpawn;
    private float _buffApplicationTime;
    private float _buffTime;

    private void Start()
    {
        instance = this;
        _stack = new BuffsStack();
        _stack.InitializeStack();
    }

    private void Update()
    {
        UIManager.instance.buffBar.SetActive(PlayerController.instance.isBuffed);
        if (PlayerController.instance.isBuffed) UIManager.instance.UpdateBuff(_buffApplicationTime, _buffTime);    
    }

    public void SetSpawnPosition(Vector2 newPosition)
    {
        Debug.Log(newPosition);
        _buffSpawn = newPosition;
        _stack.Push(buff);
        TrySpawnBuff();
    }

    private void TrySpawnBuff()
    {
        if(!PlayerController.instance.isBuffed && GameObject.FindGameObjectsWithTag("Buff").Count() == 0)
        {
            GameObject newBuff = Instantiate(_stack.Last(), _buffSpawn, Quaternion.identity);
            _stack.Pop();
        }
    }

    public void ApplyBuff(float buff, float buffTime)
    {
        Debug.Log("Aplicar buff");
        PlayerController.instance.currentDamage += buff;
        PlayerController.instance.isBuffed = true;
        _buffApplicationTime = Time.time;
        _buffTime = buffTime;
        StartCoroutine(StartCountdown(buff, buffTime));
    }

    private void RemoveBuff(float buff)
    {
        Debug.Log("Quitar buff");
        PlayerController.instance.currentDamage -=  buff;
        PlayerController.instance.isBuffed = false;
    }

    private IEnumerator StartCountdown(float buff, float buffTime)
    {
        yield return new WaitForSeconds(buffTime);
        RemoveBuff(buff);
    }
}
