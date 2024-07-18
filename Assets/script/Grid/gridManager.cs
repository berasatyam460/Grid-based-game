using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    public static gridManager instance;  // creating a singleton pattern
    [SerializeField] Vector2Int gridSize; // 10*10 gridsize
    [SerializeField] int unitGridSize; // block size of each tile if tile size is 2 then it is 2
   
    public int size { get { return unitGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();  // creating a dictionary

    public Dictionary<Vector2Int,Node> Grid { get { return grid; } }

    [SerializeField] GameObject Gridcube;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        create_Grid();
    }

    public void BlockNode(Vector2Int coordinates)  // use it if any obstable is present or not in the grid
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }


    void create_Grid()    // creating the grid from the grid size
    {

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cordinate = new Vector2Int(x, y);  
                grid.Add(cordinate, new Node(cordinate,true));

                Vector3 pos = new Vector3(cordinate.x * unitGridSize, 0, cordinate.y * unitGridSize);
                GameObject cube = Instantiate(Gridcube, pos, Quaternion.identity, this.transform);

                cube.AddComponent<GridCubeInfo>();
            }
        }
    }
}
