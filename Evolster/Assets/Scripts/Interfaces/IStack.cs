using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStack
{
    void InitializeStack();

    void Push(GameObject x);

    void Pop();

    bool EmptyStack();

    GameObject Last();
}
