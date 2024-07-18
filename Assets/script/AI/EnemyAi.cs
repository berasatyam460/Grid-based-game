using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour, AI
{
    public float moveSpeed = 2f;
    public Pathfinder pathfinder;
    public Transform playerTransform;
    public Vector2Int currentCoordinates;
    [SerializeField] Vector2Int targetCoordinates;
   gridManager gm;
    private bool isMoving = false;
    private Queue<Node> pathQueue = new Queue<Node>();

    private void Start()
    {
        gm = FindAnyObjectByType<gridManager>();
        pathfinder = new Pathfinder();
        currentCoordinates = new Vector2Int(Mathf.RoundToInt(transform.position.x)/gm.size, Mathf.RoundToInt(transform.position.z)/gm.size);
    }

    private void Update()
    {
        playerTransform = FindAnyObjectByType<PlayerController>().transform;
        if (!isMoving)
        {
            MoveToward(playerTransform.position);
        }
    }

    public void MoveToward(Vector3 targetPosition)
    {
        if (!isMoving)
        {
            targetCoordinates = GetNextTargetCoordinates(targetPosition);  //set the target cords
            List<Node> path = pathfinder.SetNewDestination(currentCoordinates, targetCoordinates);

            if (path.Count > 0)
            {
                pathQueue = new Queue<Node>(path);
                StartCoroutine(MoveAlongPath());
            }
        }
    }

    private Vector2Int GetNextTargetCoordinates(Vector3 playerPosition)  // get the target coordinate from the player by adding adjacent pos
    {
        Vector2Int playerCoords = new Vector2Int(Mathf.RoundToInt(playerPosition.x), Mathf.RoundToInt(playerPosition.z));
        Vector2Int[] adjacentPositions = new Vector2Int[]
        {
            
            playerCoords + Vector2Int.down *gm.size,
            playerCoords + Vector2Int.left*gm.size,
            playerCoords + Vector2Int.right*gm.size,
            playerCoords + Vector2Int.up*gm.size
        };  

        Vector2Int closestPosition = currentCoordinates;
        float shortestDistance = float.MaxValue;

        foreach (var pos in adjacentPositions)
        {
            Vector2Int cords = pos / gm.size;
            if (gm.Grid.ContainsKey(cords) && gm.Grid[cords].isWalkable)
            {
                float distance = Vector2.Distance(currentCoordinates, cords);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestPosition = cords;
                }
            }
        }

        return closestPosition;
    }

  

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;
        while (pathQueue.Count > 0)
        {
            Node nextNode = pathQueue.Dequeue();
            Vector3 nextPosition = new Vector3(nextNode.cords.x *gm.size, this.transform.position.y, nextNode.cords.y*gm.size);

            while ((nextPosition - this.transform.position).sqrMagnitude > 0.01f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            this.transform.position = nextPosition;
            currentCoordinates = nextNode.cords ;
        }
        isMoving = false;
    }
}
