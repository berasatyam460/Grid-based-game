using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder   // implementing A* algo
{
    Node startNode;
    Node endNode;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Vector2Int[] searchOrder = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };  // use for search the adjacent node by vector2int
 

    public List<Node> SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)  
    {
       
        grid = gridManager.instance.Grid;  //
        startNode = grid[startCoordinates];
        endNode = grid[targetCoordinates];
        return findPath(startNode, endNode);
        
    }

    public List<Node> findPath(Node start,Node end)
    {
       
        List<Node> openlist = new List<Node>();
        List<Node> closelist = new List<Node>();


        openlist.Add(start);
        while (openlist.Count > 0)
        {
            Node currentNode = openlist.OrderBy(x => x.F).First();  //  smaller F value is assign to current Node

            openlist.Remove(currentNode);
            closelist.Add(currentNode);

            if (currentNode == end)
            {
                return BuildPath(start, end); 
            }

            var neighbortile = GetNeighborTile(currentNode);
            
            foreach(var neighbor in neighbortile)
            {
                if (!neighbor.isWalkable || closelist.Contains(neighbor)){
                    continue;
                }

                neighbor.G = getDis(start, neighbor);    //calculate movement cost
                neighbor.H = getDis(end, neighbor);     // calculate estimate movement value

                neighbor.previous = currentNode;
                if (!openlist.Contains(neighbor) && neighbor.isWalkable)
                {
                    openlist.Add(neighbor);
                }
            }
        }

        return new List<Node>();
    }

    private List<Node> BuildPath(Node start, Node end) 
    {
        List<Node> finishedList = new List<Node>();
        Node currentNode = end;  // set the current node is end node
        while (currentNode != start)
        {
            finishedList.Add(currentNode);
            currentNode = currentNode.previous;
        }

        finishedList.Reverse();  // reverse the list for 
        return finishedList;
    }

    private int getDis(Node current, Node neighbor)  // use to calculate distance from current to neighbor node
    {
        return Mathf.Abs(current.cords.x - neighbor.cords.x) + Mathf.Abs(current.cords.y - neighbor.cords.y);
    }

    private List<Node> GetNeighborTile(Node currentNode)  // get the neighbor node from current node 
    {
        List<Node> neighbors = new List<Node>();

        foreach (var dir in searchOrder) 
        {
           Vector2Int locationTocheck= currentNode.cords + dir;
            if (grid.ContainsKey(locationTocheck))
            {
                neighbors.Add(grid[locationTocheck]);
            }
        }

        return neighbors;
    }
}
