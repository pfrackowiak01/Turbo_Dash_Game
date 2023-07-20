using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

[CreateAssetMenu(fileName = "WallData", menuName = "ScriptableObject/Wall")]
public class Wall : ScriptableObject, ISpawnable
{
    [Header("Wall parameters")]
    [SerializeField] private int index;
    [SerializeField] private string wallName;

    [SerializeField] private GameObject prefab;

    [SerializeField] private Location location;
    [SerializeField] private LevelDifficulty difficulty;

    public int GetIndex()
    {
        return index;
    }

    public string GetName()
    {
        return wallName;
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
