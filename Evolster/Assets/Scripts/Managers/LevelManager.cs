using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public DoorScript[] levelDoors;
    
    private void Update()
    {
        SetLevelDoors();
    }

    private void SetLevelDoors()
    {
        levelDoors = FindObjectsOfType<DoorScript>();
        for (int n = 1; n <= levelDoors.Length; n++)
            if (GameManager.instance.currentLevel >= n) levelDoors[n - 1].enabled = true;

        Debug.Log("Now");
    }
}
