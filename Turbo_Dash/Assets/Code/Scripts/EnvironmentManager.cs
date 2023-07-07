using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject tube;
    public Transform parrent;
    public float spawnRate = 1f;
    public float spawnTubeZone = 240f;
    public float deadTubeZone = -60f;
    private int tubeLength = 60;
    private GameObject[] tubesArray;

    private void Awake()
    {

    }

    void Start()
    {
        for (int i = 0; i <= 5; i++)
        {
            spawnTube(tubeLength * i);
            if (GameManager.Instance.safeTubes > 0) GameManager.Instance.safeTubes--;
        }
    }

    private void FixedUpdate()
    {
        // ZnajdŸ wszystkie obiekty z danym tagiem
        tubesArray = GameObject.FindGameObjectsWithTag("Tube");

        // Policz liczbê obiektów
        int tubeCount = tubesArray.Length;

        if (tubeCount < 5)
        {
            spawnTube(spawnTubeZone - 1);
        }

        // Przywracanie pustej tablicy
        tubesArray = null;
    }

    void Update()
    {

      

    }

    void spawnTube(float z)
    {
        Debug.Log("Spawn Tube");

        // Tworzenie nowej instancji rury z prefabu
        Instantiate(tube, new Vector3(0f, 0f, z), parrent.rotation, parrent);

    }
}
