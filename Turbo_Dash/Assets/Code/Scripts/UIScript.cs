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

    private float timer = 0f;
    private float scoreTime = 0f;
    private float distanceToLevelUp = 1000f;

    void Start()
    {
        //GameManager.Instance.GameLevelUp();
        levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();
    }

    void Update()
    {
        // ==========> EKRAN ZATRZYMANEJ GRY <==========
        if (GameManager.Instance.gamePaused)
        {
            // Pokazanie widoku zatrzymanej gry
            gamePausedScreen.SetActive(true);
        }
        // -------------> SYSTEM PUNKT�W <--------------
        else
        {
            // Ukrycie widoku zatrzymanej gry
            gamePausedScreen.SetActive(false);

            // Up�yw czasu
            timer += Time.deltaTime;

            // Obliczanie wyniku na podstawie czasu
            scoreTime = timer * 17 * (GameManager.Instance.gameLevel/10 + 1);
        }
        // ---------------------------------------------


        // ==========> EKRAN ZAKO�CZONEJ GRY <==========
        if (GameManager.Instance.gameHasEnded)
        {
            // Pokazanie widoku zako�czonej gry
            gameOverScreen.SetActive(true);
        }
        else
        {
            // Ukrycie widoku zako�czonej gry
            gameOverScreen.SetActive(false);
        }
        // =============================================


        // ==========> EKRAN ROZGRYWANEJ GRY <==========

        // Zwi�kszenie poziomu po okre�lonym pokonanym dystansie
        if (scoreTime > GameManager.Instance.gameLevel * distanceToLevelUp)
        {
            GameManager.Instance.GameLevelUp();

            // Wy�wietlanie aktualnego poziomu
            levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();
        }

        // Wy�wietlanie uzyskanego wyniku
        scoreText.text = scoreTime.ToString("0");

        // Wy�wietlanie ilo�ci z�ota
        goldText.text = "Gold: " + GameManager.Instance.playerGold.ToString();

        // Wy�wietlanie ilo�ci �y�
        livesText.text = "Lives: " + GameManager.Instance.playerLives.ToString();

        // Wy�wietlanie czy gracz posiada tarcze
        shieldText.text = "Shield:  " + GameManager.Instance.playerShield.ToString();
        // =============================================
    }
}
