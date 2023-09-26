using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue : MonoBehaviour, IQueue
{
    public GameObject[] a;
    public int index;
    public void InitializeQueue()
    {
        a = new GameObject[100];
        index = 0;
    }

    public void Enqueue(GameObject x)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            a[i + 1] = a[i];
        }
        a[0] = x;

        index++;
    }

    public void Dequeue()
    {
        index--;
    }

    public bool EmptyQueue()
    {
        return (index == 0);
    }

    public GameObject First()
    {
        return a[index - 1];
    }
}
