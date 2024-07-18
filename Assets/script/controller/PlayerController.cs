using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] float movementSpeed = 5f;  


    Transform selectedPlayer;
    bool playerselected = false;
    gridManager GridManager;
    Pathfinder pathfinder;


    [SerializeField]Vector2Int currentCords;
    [SerializeField]Vector2Int targetcords;
    private bool isMoving = false;
    public List<Node> path = new List<Node>();
    private Queue<Node> pathQueue = new Queue<Node>();

    private void Start()
    {
        GridManager = FindObjectOfType<gridManager>();
        pathfinder = new Pathfinder();
    }


    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (!isMoving&&Input.GetMouseButtonDown(0))
        {
            bool hashit = Physics.Raycast(ray, out hit);
            if (hashit)
            {
                if (hit.transform.tag == "Tile")
                {
                    if (playerselected)
                    {
                        targetcords = hit.transform.GetComponent<GridCubeInfo>().gridcords;
                        currentCords = new Vector2Int((int)selectedPlayer.position.x, (int)selectedPlayer.position.z)/ GridManager.size;
                        if (GridManager.Grid[targetcords].isWalkable)
                        {
                            List<Node> path = pathfinder.SetNewDestination(currentCords, targetcords);  // set the path  from startcord and targetcord 
                            Debug.Log(path.Count);
                            if (path.Count > 0)
                            {
                                pathQueue = new Queue<Node>(path);
                                StartCoroutine(MoveAlongPath());
                            }
                        }

                    }
                }
                if (hit.transform.tag == "Player")
                {
                    playerselected = true;
                    selectedPlayer = hit.transform;
                }
               
            }
        }

       
    }

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;
        while (pathQueue.Count > 0)
        {
            Node nextNode = pathQueue.Dequeue();   
            Vector3 nextPosition = new Vector3(nextNode.cords.x* GridManager.size, selectedPlayer.position.y, nextNode.cords.y * GridManager.size);

            while ((nextPosition - selectedPlayer.position).sqrMagnitude > 0.01f)
            {
                selectedPlayer.position = Vector3.MoveTowards(selectedPlayer.position, nextPosition, movementSpeed * Time.deltaTime);
                yield return null;
            }

            selectedPlayer.position = nextPosition;
            currentCords = nextNode.cords*GridManager.size;
        }
        isMoving = false;
    }
}
