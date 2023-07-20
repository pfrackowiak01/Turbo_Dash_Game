using System;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Game Settings")]
    public bool gameHasEnded;
    public bool gamePaused;
    public float gameLevel;
    public Theme gameTheme;
    public string gameThemeName;
    public LevelDifficulty gameInsideDifficulty;
    public LevelDifficulty gameOutsideDifficulty;
    public Location gameLocation;
    private float restartDelay = 2f;
    public bool isPortalGoingToSpawn;
    public GameObject Portal;

    [Header("Player Settings")]
    public int playerLives;
    public bool playerShield;
    public bool playerImmortality;
    public float playerGold = 0;
    public float playerHighScore = 0;
    public bool boostEffectEnable;
    public bool isFOVChanging;
    public float rotationAmount;

    [Header("Tube Settings")]
    public int safeTubes;           // 3                              // Ilo�� bezpiecznych rur bez przeszk�d na pocz�tku gry
    public float tubeLength;        // 60                             // D�ugo�� pojedynczej rury
    public float tubeDeadZone;      // -60                            // Miejsce usuni�cia rury
    public float tubeSpawnZone;     // 240                            // Miejsce pojawienia si� rury
    public float tubeMoveSpeed;     // 50                             // Aktualna pr�dko�� poruszania si� rury
    public float tubeSpawnRate;     // 1.2                            // Co ile sekund ma si� pojawia� nowa rura
    public float startMoveSpeed;    // 50                             // Pocz�tkowa pr�dko�� poruszania si� rury
    public float extraMoveSpeed;    // 0                              // Dodatkowa przyspieszenie poruszania si� rury
    public float maxExtraMoveSpeed; // 30                             // Maksymalne dodatkowa przyspieszenie rury

    [Header("Level Generation")]
    public Wall[] allWalls;                                           // Tablica wszystkich �cian
    public Obstacle[] allObstacles;                                   // Tablica wszystkich przeszk�d
    public Gem[] allGems;                                             // Tablica wszystkich gem�w
    public Theme[] allThemes;                                         // Tablica wszystkich motyw�w
    public List<Wall> usedWalls = new List<Wall>();                   // Lista aktualnie u�ywanych �cian
    public List<Obstacle> usedObstacles = new List<Obstacle>();       // Lista aktualnie u�ywanych przeszk�d
    public List<Gem> usedGems = new List<Gem>();                      // Lista aktualnie u�ywanych gem�w

    public enum LevelDifficulty
    {
        None,
        Any,
        Easy,
        Medium,
        Hard
    }

    public enum Location
    {
        Inside,
        Outside,
        Both
    }

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
        gameInsideDifficulty = LevelDifficulty.Easy;
        gameOutsideDifficulty = LevelDifficulty.Hard;
        gameLocation = Location.Inside;
        isPortalGoingToSpawn = false;

        playerLives = 3;
        playerShield = false;
        playerImmortality = false;
        boostEffectEnable = false;
        isFOVChanging = false;
        rotationAmount = 0;

        safeTubes = 3;
        tubeLength = 60f;
        tubeDeadZone = -tubeLength;
        tubeSpawnZone = tubeLength * 4f;
        startMoveSpeed = 50f;
        tubeMoveSpeed = startMoveSpeed;
        tubeSpawnRate = tubeLength / tubeMoveSpeed;
        extraMoveSpeed = 0f;
        maxExtraMoveSpeed = 30f;

        usedWalls = UpdateObjectListToSpawnByLevel(allWalls);
        usedObstacles = UpdateObjectListToSpawnByLevel(allObstacles);
        usedGems = UpdateObjectListToSpawnByLevel(allGems);

        gameTheme = GetThemeByName(gameThemeName);
        RenderSettings.skybox = gameTheme.Space;
    }

    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Time.timeScale = 0.4f;
            Debug.Log("GAME OVER");
            Invoke("RestartGame",restartDelay);
        }
    }

    public void RestartGame()
    {
        // Zresetowanie post�pu
        usedWalls = new List<Wall>();
        usedObstacles = new List<Obstacle>();
        usedGems = new List<Gem>();
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
        // Zwi�kszenie levelu, definiowanie poziomu
        gameLevel++;
        ChooseGameDifficultyByLocation();

        // Obliczanie nowej pr�dko�ci poruszania si� rur
        if (extraMoveSpeed < maxExtraMoveSpeed) extraMoveSpeed += 5f;
        tubeMoveSpeed = startMoveSpeed + extraMoveSpeed;

        // Korygowanie czasu w jakim rury musz� si� spawnowa�, aby zachowa� odpowiedni� odleg�o��
        tubeSpawnRate = tubeLength / tubeMoveSpeed;

        // Nadpisywanie list nowymi listami dla aktualnego levelu
        usedWalls = UpdateObjectListToSpawnByLevel(allWalls);
        usedObstacles = UpdateObjectListToSpawnByLevel(allObstacles);
        usedGems = UpdateObjectListToSpawnByLevel(allGems);
    }

    public void ChooseGameDifficultyByLocation()
    {
        // SPACE Level - Outside
        if (gameLocation == Location.Outside)
        {
            switch (gameOutsideDifficulty)
            {
                case LevelDifficulty.Easy:
                    gameOutsideDifficulty = LevelDifficulty.Medium;
                    break;
                case LevelDifficulty.Medium:
                    gameOutsideDifficulty = LevelDifficulty.Hard;
                    break;
                case LevelDifficulty.Hard:
                    gameOutsideDifficulty = LevelDifficulty.Easy;
                    break;
            }
        }
        // DEFAULT Level - Inside
        else
        {
            switch (gameInsideDifficulty)
            {
                case LevelDifficulty.Easy:
                    gameInsideDifficulty = LevelDifficulty.Medium;
                    break;
                case LevelDifficulty.Medium:
                    gameInsideDifficulty = LevelDifficulty.Hard;
                    break;
                case LevelDifficulty.Hard:
                    gameInsideDifficulty = LevelDifficulty.Easy;
                    break;
            }
        }
    }

    // Funkcj� z wykorzystaniem parametru typu generycznego, kt�ra b�dzie akceptowa� dowoln� klas� implementuj�c� interfejs ISpawnable
    public List<T> UpdateObjectListToSpawnByLevel<T>(T[] allObjects) where T : ISpawnable
    {
        List<T> updatedObjects = new List<T>();

        // Przechodzi przez wszystkie obiekty
        foreach (var obj in allObjects)
        {
            // Sprawdza, czy obiekt ma ten sam poziom trudno�ci i lokacje co aktualny stan gry
            if ((obj.GetLevelDifficulty() == gameInsideDifficulty || obj.GetLevelDifficulty() == LevelDifficulty.Any) &&
                (obj.GetLocation() == gameLocation || obj.GetLocation() == Location.Both))
            {
                // Dodaje obiekt do listy
                updatedObjects.Add(obj);
            }
        }

        return updatedObjects;
    }

    public Theme GetThemeByName(string themeNameToFind)
    {
        // Przeszukujemy wszystkie motywy w poszukiwaniu tej w�a�ciwej
        foreach (Theme theme in allThemes)
        {
            if (theme.ThemeName == themeNameToFind) return theme;
        }

        // Je�li nie znaleziono motywu o podanej nazwie, zwracamy domy�lny motyw
        return allThemes.FirstOrDefault(theme => theme.ThemeName == "Default");
    }

    public void SpawnObject<T>(List<T> objectList, Transform parent) where T : ISpawnable
    {
        if (objectList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, objectList.Count);
            ISpawnable spawnableObject = objectList[randomIndex];
            GameObject newObject = Instantiate(spawnableObject.GetPrefab(), parent.position, parent.rotation, parent);
            AssignMaterialByTag(newObject);
        }
        else Debug.Log("Lista obiekt�w jest pusta!");
    }
    public void SpawnPortal(Transform parent)
    {
        GameObject newObject = Instantiate(Portal, parent.position, parent.rotation, parent);
        //AssignMaterialByTag(newObject);
    }


    private void AssignMaterialByTag(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Renderer[] childRenderers = obj.GetComponentsInChildren<Renderer>();

        if (renderer != null)
        {
            if (obj.CompareTag("Obstacle")) renderer.material = gameTheme.Obstacle;
            else if (obj.CompareTag("Wall")) renderer.material = gameTheme.Wall;
        }
        else if (childRenderers != null)
        {
            foreach (Renderer childRenderer in childRenderers)
            {
                if (obj.CompareTag("Obstacle")) childRenderer.material = gameTheme.Obstacle;
                else if (obj.CompareTag("Wall")) childRenderer.material = gameTheme.Wall;
            }
        }
    }

    public void ToggleVisibilityWithTag(string tagToToggle)
    {
        GameObject[] objectsToToggle = GameObject.FindGameObjectsWithTag(tagToToggle);

        foreach (GameObject obj in objectsToToggle)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null) renderer.enabled = !renderer.enabled;
        }
    }
}
