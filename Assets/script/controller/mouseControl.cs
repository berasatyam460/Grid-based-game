using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mouseControl : MonoBehaviour
{
    [SerializeField] TMP_Text cubeInfoText;
    [SerializeField] GameObject player;[SerializeField] GameObject enemy;
    bool isplayerinscene = false;bool isenemyinscene = false;
    [SerializeField]gridManager GridManager;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GridCubeInfo tileInfo = hit.transform.GetComponent<GridCubeInfo>();
            if (tileInfo != null)
            {
                transform.position = new Vector3(tileInfo.gridcords.x * GridManager.size, 0.05f, tileInfo.gridcords.y * GridManager.size);  //for cursor movement
                cubeInfoText.text = $"Tile Position: ({tileInfo.gridcords.x}, {tileInfo.gridcords.y})";
            }
        }

        if (!isplayerinscene && Input.GetMouseButtonDown(0))  // on left click generate a player
        {
            
            Vector2Int targetcords = hit.transform.GetComponent<GridCubeInfo>().gridcords;
            if (GridManager.Grid[targetcords].isWalkable)
            {
                isplayerinscene = true;
                var player = Instantiate(this.player, new Vector3(targetcords.x * GridManager.size, 1f, targetcords.y * GridManager.size), Quaternion.identity);
            }
            else
            {
                Debug.LogError("obstacle");
            }
            
        }

        if(!isenemyinscene && Input.GetMouseButtonDown(1))  // on right click generate an enemy
        {
            
            Vector2Int targetcords = hit.transform.GetComponent<GridCubeInfo>().gridcords;
            if (GridManager.Grid[targetcords].isWalkable)
            {
                isenemyinscene = true;
                var enemy = Instantiate(this.enemy, new Vector3(targetcords.x * GridManager.size, 1f, targetcords.y * GridManager.size), Quaternion.identity);
            }
            else
            {
                Debug.LogError("obstacle");
            }
        }
    }
}
