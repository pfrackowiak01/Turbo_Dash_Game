using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI turboText;
    public TextMeshProUGUI gameModeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI diamondsText;
    public TextMeshProUGUI livesText;
    public Image shield;
    public GameObject gamePausedScreen;
    public GameObject gameOverScreen;

    public float speed;
    public float baseSpeed;
    public float maxSpeed;
    public float turboSpeed;

    private float previousScore;
    private float previousTime;
    public float updateInterval = 0.5f; // Co ile sekund ma by� aktualizowany wynik

    void Start()
    {
        speed = 0;
        baseSpeed = 20;
        maxSpeed = 20;
        turboSpeed = 20;

        previousScore = GameManager.Instance.gameScore;
        previousTime = Time.time;
    }

    void Update()
    {
        // ==========> EKRAN ZATRZYMANEJ GRY <==========
        if (GameManager.Instance.gamePaused)
        {
            // Pokazanie widoku zatrzymanej gry
            gamePausedScreen.SetActive(true);
        }
        // ==========> EKRAN ZAKO�CZONEJ GRY <==========
        else if (GameManager.Instance.gameHasEnded)
        {
            // Pokazanie widoku zako�czonej gry
            gameOverScreen.SetActive(true);
        }
        // -----------> EKRAN AKTYWNEJ GRY <------------
        else
        {
            // Ukrycie widoku zatrzymanej gry
            gamePausedScreen.SetActive(false);

            // Ukrycie widoku zako�czonej gry
            gameOverScreen.SetActive(false);
        }
        // --------------------------------------------

        // Obliczanie pr�dko�ci i wy�wietlanie jej w p�ynny spos�b
        //if (GameManager.Instance.boostEffectEnable) maxSpeed = baseSpeed + turboSpeed;
        //else maxSpeed = baseSpeed;
        //if (speed < maxSpeed) speed += 0.1f;
        //if (speed > maxSpeed) speed -= 0.1f;

        // =========> OBLICZANIE PR�DKO�CI m/s <========
        float currentScore = GameManager.Instance.gameScore;
        float currentTime = Time.time;

        // Sprawdzamy, czy min�a wystarczaj�ca ilo�� czasu do aktualizacji wyniku
        if (currentTime - previousTime >= updateInterval)
        {
            // Obliczamy r�nic� w punktach i czasie od ostatniej aktualizacji
            float scoreDifference = currentScore - previousScore;
            float timeDifference = currentTime - previousTime;

            // Obliczamy punkty na sekund� (punkty/s)
            float scorePerSecond = scoreDifference / timeDifference;

            // Wy�wietlamy wynik na ekranie
            speed = scorePerSecond;

            // Aktualizujemy warto�ci poprzednich wynik�w i czasu
            previousScore = currentScore;
            previousTime = currentTime;
        }

        // ==========> EKRAN ROZGRYWANEJ GRY <==========
        // Wy�wietlanie uzyskanego wyniku
        scoreText.text = GameManager.Instance.gameScore.ToString("0");

        // Wy�wietlanie aktualnej pr�dko�ci
        speedText.text = speed.ToString("F1");

        // Wy�wietlanie aktualnego stanu TURBO
        turboText.text = "TURBO";

        // Wy�wietlanie aktualnego poziomu
        //gameModeText.text = SaveAndLoadManager.Instance.GameModeString();

        // Wy�wietlanie aktualnego poziomu
        levelText.text = GameManager.Instance.gameLevel.ToString();

        // Wy�wietlanie ilo�ci z�ota
        coinsText.text = GameManager.Instance.playerCoins.ToString();

        // Wy�wietlanie ilo�ci diament�w
        diamondsText.text = GameManager.Instance.playerDiamonds.ToString();

        // Wy�wietlanie ilo�ci �y�
        livesText.text = GameManager.Instance.playerLives.ToString();

        // Wy�wietlanie czy gracz posiada tarcze
        if (GameManager.Instance.playerShield) shield.enabled = true;
        else shield.enabled = false;
        // =============================================
    }
}
