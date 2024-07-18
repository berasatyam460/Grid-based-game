using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] ObstacleData obstacleData;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] gridManager GM;
    private void Start()
    {
        for (int x = 0; x <10; x++)
        {
            for (int y = 0; y <10; y++)
            {
                Vector2Int cordinate = new Vector2Int(x, y);
                int index = x * 10 + y;
                if (obstacleData.obstacles[index])  // 
                {
                    Vector3 position = new Vector3(x * GM.size, 0.5f, y * GM.size); // set the coords
                    GM.BlockNode(cordinate);//block the coordinate
                    var sphere=Instantiate(obstaclePrefab, position, Quaternion.identity,this.transform);  // 
                }
                
            }
        }
    }
   
}
