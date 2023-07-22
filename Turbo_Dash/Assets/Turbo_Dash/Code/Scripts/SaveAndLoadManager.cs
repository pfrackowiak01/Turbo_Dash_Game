using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadManager : MonoBehaviour
{

    // ===================== SINGLETON =====================
    // Deklaruje w³aœciwoœæ statyczn¹ o nazwie "Instance".
    public static SaveAndLoadManager Instance { get; private set; }

    // Sprawdza, czy ju¿ istnieje instancja SaveAndLoadManager.
    // Jeœli nie, ustawia wartoœæ w³aœciwoœci "Instance" na bie¿¹c¹ instancjê (this).
    // Mo¿emy siê do niej odwo³ywaæ przez "SaveAndLoadManager.Instance".
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

    public int gyroscopeHighScore;
    public int touchControlHighScore;
    public int multiplayerHighScore;

    public GameMode gameMode;

    public enum GameMode
    {
        Gyroscope,
        TouchControl,
        Multiplayer
    }

    // ----------------- GLOBAL FUNCTIONS ------------------
    void Start()
    {
        gyroscopeHighScore = PlayerPrefs.GetInt("gyroscopeHighScore", 0);
        touchControlHighScore = PlayerPrefs.GetInt("touchControlHighScore", 0);
        multiplayerHighScore = PlayerPrefs.GetInt("multiplayerHighScore", 0);

        gameMode = GameMode.Gyroscope;
    }

    public void SaveHighScore(int highscore)
    {
        switch (gameMode)
        {
            case GameMode.Gyroscope:
                if (highscore > gyroscopeHighScore) PlayerPrefs.SetInt("gyroscopeHighScore", highscore);
                break;
            case GameMode.TouchControl:
                if (highscore > touchControlHighScore) PlayerPrefs.SetInt("touchControlHighScore", highscore);
                break;
            case GameMode.Multiplayer:
                if (highscore > multiplayerHighScore) PlayerPrefs.SetInt("multiplayerHighScore", highscore);
                break;
            default:
                Debug.LogWarning("Nieprawid³owy tryb gry przy próbie zapisu highscora!");
                break;
        }
    }

    public string GameModeString()
    {
        switch (gameMode)
        {
            case GameMode.Gyroscope:
                return "Gyroscope";
            case GameMode.TouchControl:
                return "TouchControl";
            case GameMode.Multiplayer:
                return "Multiplayer";
            default:
                return "GameMode";
        }
    }
}
