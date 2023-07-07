using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ===================== SINGLETON =====================
    // Deklaruje w³aœciwoœæ statyczn¹ o nazwie "Instance".
    public static GameManager Instance { get; private set; }

    // Sprawdza, czy ju¿ istnieje instancja GameManager.
    // Jeœli nie, ustawia wartoœæ w³aœciwoœci "Instance" na bie¿¹c¹ instancjê (this).
    // Mo¿emy siê do niej odwo³ywaæ przez "GameManager.Instance".
    private void Awake()
    {
        // Obiekt nie zostanie zniszczony podczas przejœcia miêdzy scenami.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Niszczy nowo utworzony obiekt GameManager, aby zachowaæ tylko jedn¹ instancje w grze.
        else Destroy(gameObject);
    }
    // =====================================================

    // ---------------------- GLOBALS ----------------------
    public bool gameHasEnded = false;
    public bool gamePaused = true;
    public float restartDelay = 1f;
    public float gameLevel = 0;
    public int playerLives = 2;
    public bool playerShield = false;
    public int safeTubes = 2;

    public GameObject[] allWallPrefabs;                               // Tablica prefabów œcian
    public GameObject[] allObstaclePrefabs;                           // Tablica prefabów przeszkód
    public List<GameObject> wallPrefabs = new List<GameObject>();     // Lista aktualnie u¿ywanych œcian
    public List<GameObject> obstaclePrefabs = new List<GameObject>(); // Lista aktualnie u¿ywanych przeszkód

    // ----------------- GLOBAL FUNCTIONS ------------------
    public void Start()
    {
        wallPrefabs.AddRange(FilterPrefabsByPrefix(allWallPrefabs, "lv1"));
        obstaclePrefabs.AddRange(FilterPrefabsByPrefix(allObstaclePrefabs, "lv1"));
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameHasEnded = false;
        playerLives = 2;
    }

    public void GameLevelUp()
    {
        gameLevel++;
        wallPrefabs.AddRange(FilterPrefabsByPrefix(allWallPrefabs, "lv" + gameLevel));
        obstaclePrefabs.AddRange(FilterPrefabsByPrefix(allObstaclePrefabs, "lv" + gameLevel));
    }

    public List<GameObject> FilterPrefabsByPrefix(GameObject[] allPrefabs, string prefix)
    {
        // Lista prefabów, które maj¹ odpowiedni prefiks
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
        // Losowanie jednego indeksu prefabu œciany
        int randomIndex = Random.Range(0, objectPrefabs.Count);

        // Wygeneruj nowy obiekt na podstawie wylosowanego prefabu
        GameObject newObject = Instantiate(objectPrefabs[randomIndex], parrent.position, parrent.rotation, parrent);
    }
}
