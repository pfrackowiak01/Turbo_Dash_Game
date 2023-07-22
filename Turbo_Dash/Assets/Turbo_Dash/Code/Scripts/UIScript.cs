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
    public float updateInterval = 0.5f; // Co ile sekund ma byæ aktualizowany wynik

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
        // ==========> EKRAN ZAKOÑCZONEJ GRY <==========
        else if (GameManager.Instance.gameHasEnded)
        {
            // Pokazanie widoku zakoñczonej gry
            gameOverScreen.SetActive(true);
        }
        // -----------> EKRAN AKTYWNEJ GRY <------------
        else
        {
            // Ukrycie widoku zatrzymanej gry
            gamePausedScreen.SetActive(false);

            // Ukrycie widoku zakoñczonej gry
            gameOverScreen.SetActive(false);
        }
        // --------------------------------------------

        // Obliczanie prêdkoœci i wyœwietlanie jej w p³ynny sposób
        //if (GameManager.Instance.boostEffectEnable) maxSpeed = baseSpeed + turboSpeed;
        //else maxSpeed = baseSpeed;
        //if (speed < maxSpeed) speed += 0.1f;
        //if (speed > maxSpeed) speed -= 0.1f;

        // =========> OBLICZANIE PRÊDKOŒCI m/s <========
        float currentScore = GameManager.Instance.gameScore;
        float currentTime = Time.time;

        // Sprawdzamy, czy minê³a wystarczaj¹ca iloœæ czasu do aktualizacji wyniku
        if (currentTime - previousTime >= updateInterval)
        {
            // Obliczamy ró¿nicê w punktach i czasie od ostatniej aktualizacji
            float scoreDifference = currentScore - previousScore;
            float timeDifference = currentTime - previousTime;

            // Obliczamy punkty na sekundê (punkty/s)
            float scorePerSecond = scoreDifference / timeDifference;

            // Wyœwietlamy wynik na ekranie
            speed = scorePerSecond;

            // Aktualizujemy wartoœci poprzednich wyników i czasu
            previousScore = currentScore;
            previousTime = currentTime;
        }

        // ==========> EKRAN ROZGRYWANEJ GRY <==========
        // Wyœwietlanie uzyskanego wyniku
        scoreText.text = GameManager.Instance.gameScore.ToString("0");

        // Wyœwietlanie aktualnej prêdkoœci
        speedText.text = speed.ToString("F1");

        // Wyœwietlanie aktualnego stanu TURBO
        turboText.text = "TURBO";

        // Wyœwietlanie aktualnego poziomu
        //gameModeText.text = SaveAndLoadManager.Instance.GameModeString();

        // Wyœwietlanie aktualnego poziomu
        levelText.text = GameManager.Instance.gameLevel.ToString();

        // Wyœwietlanie iloœci z³ota
        coinsText.text = GameManager.Instance.playerCoins.ToString();

        // Wyœwietlanie iloœci diamentów
        diamondsText.text = GameManager.Instance.playerDiamonds.ToString();

        // Wyœwietlanie iloœci ¿yæ
        livesText.text = GameManager.Instance.playerLives.ToString();

        // Wyœwietlanie czy gracz posiada tarcze
        if (GameManager.Instance.playerShield) shield.enabled = true;
        else shield.enabled = false;
        // =============================================
    }
}
