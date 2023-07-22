using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject tube;
    public Transform parrent;
    private float timer;
    private GameManager.Location locationNow;
    private GameManager.Location locationBefore;
    private bool spawnBlock;

    void Start()
    {
        locationNow = GameManager.Instance.gameLocation;
        locationBefore = GameManager.Instance.gameLocation;
        RestartEnvironment();
        spawnBlock = false;
    }

    private void FixedUpdate()
    {
        locationNow = GameManager.Instance.gameLocation;

        if (locationNow != locationBefore) RestartEnvironment();

        timer += Time.deltaTime;

        if (timer >= GameManager.Instance.tubeSpawnRate - 0.01f)
        {
            if (!spawnBlock) spawnTube(GameManager.Instance.tubeSpawnZone);
            timer = 0;
        }

        locationBefore = locationNow;
    }

    private void spawnTube(float z)
    {
        Debug.Log("Spawn Tube");

        if (!GameManager.Instance.isPortalGoingToSpawn)
        {
            // Tworzenie nowej instancji rury z prefabu
             Instantiate(tube, new Vector3(0f, 0f, z), parrent.rotation, parrent);
        }
        else
        {
            // Tworzenie nowej instancji rury z prefabu
            if (!spawnBlock) Instantiate(tube, new Vector3(0f, 0f, z), parrent.rotation, parrent);
            spawnBlock = true;
        }
    }

    private void RestartEnvironment()
    {
        // Odblokuj spawnowanie siê rur
        spawnBlock = false;

        // Zniszczenie wszystkich rur
        GameObject[] tubes = GameObject.FindGameObjectsWithTag("Tube");
        if (tubes != null) foreach (GameObject tube in tubes) Destroy(tube);

        // Zespawnowanie nowych rur odpowiednich do lokacji
        for (int i = 0; i < 5; i++)
        {
            spawnTube(GameManager.Instance.tubeLength * i);
        }
        timer = 0;
    }
}
