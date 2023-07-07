using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TubeManager : MonoBehaviour
{

    public Transform parrent;

    private void Start()
    {
        if (GameManager.Instance.safeTubes == 0)
        {
            // Stwórz prefab œciany
            GameManager.Instance.SpawnObject(GameManager.Instance.wallPrefabs, parrent);

            // Stwórz prefab przeszkody
            //GameManager.Instance.SpawnObject(GameManager.Instance.obstaclePrefabs, parrent);
        }

    }

}
