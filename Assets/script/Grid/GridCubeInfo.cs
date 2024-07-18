using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridCubeInfo : MonoBehaviour
{
   [SerializeField]public Vector2Int gridcords = new Vector2Int();  // use for setting grid cords
    gridManager GridManager;
    

    private void Awake()
    {
        GridManager = FindAnyObjectByType<gridManager>();
        setgridcords();
    }

    private void setgridcords() // set the grid info like position of a grid
    {
        if (!GridManager) return;
        int x = (int)transform.position.x;
        int z = (int)transform.position.z;

        gridcords = new Vector2Int(x / GridManager.size, z / GridManager.size); 

        
    }


}
