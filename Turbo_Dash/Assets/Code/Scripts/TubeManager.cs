using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TubeManager : MonoBehaviour
{

    private void Start()
    {
        // Stwórz prefab œciany
        GameManager.Instance.SpawnObject(GameManager.Instance.wallPrefabs,transform);

        // Stwórz prefab przeszkody
        //GameManager.Instance.SpawnObject(GameManager.Instance.obstaclePrefabs, transform);

    }

}
