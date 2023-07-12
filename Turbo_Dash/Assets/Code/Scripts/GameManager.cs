using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // ===================== SINGLETON =====================
    // Deklaruje w�a�ciwo�� statyczn� o nazwie "Instance".
    public static GameManager Instance { get; private set; }

    // Sprawdza, czy ju� istnieje instancja GameManager.
    // Je�li nie, ustawia warto�� w�a�ciwo�ci "Instance" na bie��c� instancj� (this).
    // Mo�emy si� do niej odwo�ywa� przez "GameManager.Instance".
    private void Awake()
    {
        // Obiekt nie zostanie zniszczony podczas przej�cia mi�dzy scenami.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Niszczy nowo utworzony obiekt GameManager, aby zachowa� tylko jedn� instancje w grze.
        else Destroy(gameObject);
    }
    // =====================================================


    // ---------------------- GLOBALS ----------------------
    public bool gameHasEnded;
    public bool gamePaused;
    public float gameLevel;
    private float restartDelay = 2f;
    
    public int playerLives;
    public bool playerShield;
    public float playerGold = 0;
    public float playerHighScore = 0;
    public bool boostEffectEnable;
    public bool isFOVChanging;
    public float rotationAmount;

    public int safeTubes;       // 3                                  // Ilo�� bezpiecznych rur bez przeszk�d na pocz�tku gry
    public float tubeLength;    // 60                                 // D�ugo�� pojedynczej rury
    public float tubeDeadZone;  // -60                                // Miejsce usuni�cia rury
    public float tubeSpawnZone; // 240                                // Miejsce pojawienia si� rury
    public float tubeMoveSpeed; // 50                                 // Pr�dko�� poruszania si� rury
    public float tubeSpawnRate; // 1.2                                // Co ile sekund ma si� pojawi� nowa rura

    public GameObject[] allWallPrefabs;                               // Tablica prefab�w �cian
    public GameObject[] allObstaclePrefabs;                           // Tablica prefab�w przeszk�d
    public GameObject[] allGemPrefabs;                                // Tablica prefab�w gem�w
    public List<GameObject> wallPrefabs = new List<GameObject>();     // Lista aktualnie u�ywanych �cian
    public List<GameObject> obstaclePrefabs = new List<GameObject>(); // Lista aktualnie u�ywanych przeszk�d
    public List<GameObject> gemPrefabs = new List<GameObject>();      // Lista aktualnie u�ywanych gem�w


    // ----------------- GLOBAL FUNCTIONS ------------------
    public void Start()
    {
        // Ustawienie pocz�tkowych warto�ci
        StartGame();

        // Przypisanie odpowiednich obiekt�w w AnimationManager
        AnimationManager.Call.Presets();
    }

    public void StartGame()
    {
        gameHasEnded = false;
        gamePaused = true;
        gameLevel = 1;

        playerLives = 3;
        playerShield = false;
        boostEffectEnable = false;
        isFOVChanging = false;
        rotationAmount = 0;

        safeTubes = 3;
        tubeLength = 60f;
        tubeDeadZone = -tubeLength;
        tubeSpawnZone = tubeLength * 4f;
        tubeMoveSpeed = 50f;
        tubeSpawnRate = tubeLength / tubeMoveSpeed;

        wallPrefabs.AddRange(FilterPrefabsByPrefix(allWallPrefabs, "lv1_"));
        obstaclePrefabs.AddRange(FilterPrefabsByPrefix(allObstaclePrefabs, "lv1_"));
        gemPrefabs.AddRange(FilterPrefabsByPrefix(allGemPrefabs, ""));
    }

    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("RestartGame",restartDelay);
        }
    }

    public void RestartGame()
    {
        // Zresetowanie post�pu
        wallPrefabs = new List<GameObject>();
        obstaclePrefabs = new List<GameObject>();
        gemPrefabs = new List<GameObject>();
        gameHasEnded = false;

        // Ustawienie pocz�tkowych warto�ci
        StartGame();

        // Uruchomienie gry od nowa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Przypisanie odpowiednich obiekt�w w AnimationManager
        AnimationManager.Call.Presets();
    }

    public void GameLevelUp()
    {
        gameLevel++;
        tubeMoveSpeed = tubeMoveSpeed * 1.1f;
        tubeSpawnRate = tubeLength / tubeMoveSpeed;
        wallPrefabs.AddRange(FilterPrefabsByPrefix(allWallPrefabs, "lv" + gameLevel + "_"));
        obstaclePrefabs.AddRange(FilterPrefabsByPrefix(allObstaclePrefabs, "lv" + gameLevel + "_"));
        //gemPrefabs.AddRange(FilterPrefabsByPrefix(allGemPrefabs, "lv" + gameLevel  + "_"));
    }

    public List<GameObject> FilterPrefabsByPrefix(GameObject[] allPrefabs, string prefix)
    {
        // Lista prefab�w, kt�re maj� odpowiedni prefiks
        var filteredPrefabs = new List<GameObject>();

        // Przechodzi przez wszystkie prefaby
        foreach (var prefab in allPrefabs)
        {
            // Sprawdza, czy prefab ma odpowiedni prefiks w nazwie
            if (prefab.name.StartsWith(prefix))
            {
                // Dodaje prefab do listy
                filteredPrefabs.Add(prefab);
            }
        }

        return filteredPrefabs;
    }

    public void SpawnObject(List<GameObject> objectPrefabs, Transform parrent)
    {
        // Losowanie jednego indeksu prefabu �ciany
        int randomIndex = Random.Range(0, objectPrefabs.Count);

        // Wygeneruj nowy obiekt na podstawie wylosowanego prefabu
        GameObject newObject = Instantiate(objectPrefabs[randomIndex], parrent.position, parrent.rotation, parrent);
    }

    public void ToggleVisibilityWithTag(string tagToToggle)
    {
        GameObject[] objectsToToggle = GameObject.FindGameObjectsWithTag(tagToToggle);

        foreach (GameObject obj in objectsToToggle)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = !renderer.enabled;
            }
        }
    }
}
