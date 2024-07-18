using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{

    public int G;
    public int H;
    public int F { get { return G + H; } }
    public Vector2Int cords;
    public   bool isWalkable;
    public Node previous;

    public Node (Vector2Int cords, bool isWalkable)
    {
        this.cords = cords;
        this.isWalkable = isWalkable;
    }
}
