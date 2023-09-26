using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ATDQueue
{
    void InitializeQueue();

    void Enqueue(GameObject x);

    void Dequeue();

    bool EmptyQueue();

    GameObject First();
}
