using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObject/Obstacle")]
public class Obstacle : ScriptableObject, ISpawnable
{
    [Header("Obstacle parameters")]
    public int index;
    public string obstacleName;

    public GameObject prefab;

    public Location location;
    public LevelDifficulty difficulty;

    public int GetIndex()
    {
        return index;
    }

    public string GetName()
    {
        return obstacleName;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public Location GetLocation()
    {
        return location;
    }

    public LevelDifficulty GetLevelDifficulty()
    {
        return difficulty;
    }
}
