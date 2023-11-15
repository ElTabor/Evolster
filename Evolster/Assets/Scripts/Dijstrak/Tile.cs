using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile: MonoBehaviour
{
    public bool walkable;

    public Tile(bool walkable)
    {
        this.walkable = walkable;
    }
}
