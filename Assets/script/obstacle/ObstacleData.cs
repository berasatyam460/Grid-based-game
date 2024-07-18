using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacleData", menuName = "ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public bool[] obstacles = new bool[100]; // 10x10 grid

}
