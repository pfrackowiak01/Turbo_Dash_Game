using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TubeManager : MonoBehaviour
{

    private void Start()
    {
        // Stw�rz prefab �ciany
        GameManager.Instance.SpawnObject(GameManager.Instance.wallPrefabs,transform);

        // Stw�rz prefab przeszkody
        //GameManager.Instance.SpawnObject(GameManager.Instance.obstaclePrefabs, transform);

    }

}
