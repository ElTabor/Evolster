using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueue
{
    void InitializeQueue();

    void Enqueue(GameObject x);

    void Dequeue();

    bool EmptyQueue();

    GameObject First();
}
