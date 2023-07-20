using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

[CreateAssetMenu(fileName = "GemData", menuName = "ScriptableObject/Gem")]
public class Gem : ScriptableObject, ISpawnable
{
    [Header("Gem parameters")]
    public int index;
    public string gemName;

    public GameObject prefab;

    public Location location;
    public LevelDifficulty difficulty;

    public int GetIndex()
    {
        return index;
    }

    public string GetName()
    {
        return gemName;
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
