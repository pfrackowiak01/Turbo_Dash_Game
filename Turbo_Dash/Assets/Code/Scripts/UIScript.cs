using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI goldText;
    public GameObject gamePausedScreen;
    public GameObject gameOverScreen;
    public float levelTime = 60f;
    private float timerLevel = 0f;
    private float timer = 0f;
    private float scoreTime = 0f;

    void Start()
    {
        //GameManager.Instance.GameLevelUp();
        levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();
    }

    void Update()
    {
        // ==========> EKRAN ZATRZYMANEJ GRY <==========
        if (!GameManager.Instance.gamePaused)
        {
            // Ukrycie widoku zatrzymanej gry
            gamePausedScreen.SetActive(false);

            // Up�yw czasu
            timer += Time.deltaTime;
            timerLevel += Time.deltaTime;

            // Obliczanie wyniku na podstawie czasu
            scoreTime = timer * 13;
        }
        else
        {
            // Pokazanie widoku zatrzymanej gry
            gamePausedScreen.SetActive(true);
        }

        // ==========> EKRAN ZAKO�CZONEJ GRY <==========
        if (!GameManager.Instance.gameHasEnded)
        {
            // Ukrycie widoku zako�czonej gry
            gameOverScreen.SetActive(false);
        }
        else
        {
            // Pokazanie widoku zako�czonej gry
            gameOverScreen.SetActive(true);
        }

        // ==========> EKRAN ROZGRYWANEJ GRY <==========

        // Zwi�kszanie poziomu po okre�lonym czasie i jego wy�wietlanie
        if (timerLevel > levelTime)
        {
            GameManager.Instance.GameLevelUp();
            levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();
            timerLevel = 0f;
        }

        // Wy�wietlanie uzyskanego wyniku
        scoreText.text = scoreTime.ToString("0");

        // Wy�wietlanie ilo�ci z�ota
        goldText.text = "Gold: " + GameManager.Instance.playerGold.ToString();

        // Wy�wietlanie ilo�ci �y�
        livesText.text = "Lives: " + GameManager.Instance.playerLives.ToString();

        // Wy�wietlanie czy gracz posiada tarcze
        shieldText.text = "Shield:  " + GameManager.Instance.playerShield.ToString();
    }
}
