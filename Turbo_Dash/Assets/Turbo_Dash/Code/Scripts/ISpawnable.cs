using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static GameManager;

public interface ISpawnable
{
    public int GetIndex();

    public string GetName();

    public GameObject GetPrefab();

    LevelDifficulty GetLevelDifficulty();

    Location GetLocation();
}
