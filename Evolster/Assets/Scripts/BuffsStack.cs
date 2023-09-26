using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class BuffsStack : MonoBehaviour, IStack
{
    public GameObject[] a;
    public int index;

    public void InitializeStack()
    {
        a = new GameObject[100];
        index = 0;
    }

    public void Push(GameObject x)
    {
        for(int n = index -1; n >= 0; n--) a[n + 1] = a[n];
        a[0] = x;
        index++;
    }

    public void Pop() => index--;

    public bool EmptyStack()
    {
        return (index == 0);
    }

    public GameObject Last()
    {
        return a[index - 1];
    }
}
