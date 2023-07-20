using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject tube;
    public Transform parrent;
    private float timer = 0;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnTube(GameManager.Instance.tubeLength * i);
        }
    }

    private void FixedUpdate()
    {

        timer += Time.deltaTime;

        if (timer >= GameManager.Instance.tubeSpawnRate - 0.01f)
        {
            spawnTube(GameManager.Instance.tubeSpawnZone);
            timer = 0;
        }
        
        
    }

    private void spawnTube(float z)
    {
        Debug.Log("Spawn Tube");

        // Tworzenie nowej instancji rury z prefabu
        Instantiate(tube, new Vector3(0f, 0f, z), parrent.rotation, parrent);

    }
}
