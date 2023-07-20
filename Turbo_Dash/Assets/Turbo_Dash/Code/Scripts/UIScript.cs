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
    private float distanceToSpawnPortal = 760f;

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
        // ==========> EKRAN ZAKO�CZONEJ GRY <==========
        else if (GameManager.Instance.gameHasEnded)
        {
            // Pokazanie widoku zako�czonej gry
            gameOverScreen.SetActive(true);
        }
        // -------------> SYSTEM PUNKT�W <--------------
        else
        {
            // Ukrycie widoku zatrzymanej gry
            gamePausedScreen.SetActive(false);

            // Ukrycie widoku zako�czonej gry
            gameOverScreen.SetActive(false);

            // Up�yw czasu
            timer += Time.deltaTime;

            // Obliczanie wyniku na podstawie czasu
            scoreTime = timer * 23 * (GameManager.Instance.gameLevel/10 + 1);

            // Premia od efektu przyspieszenia "Boost"
            if (GameManager.Instance.boostEffectEnable) scoreTime += 1;
        }
        // ---------------------------------------------





        // ==========> EKRAN ROZGRYWANEJ GRY <==========

        // Zespawnowanie Portalu na okre�lonym dystansie
        if (!GameManager.Instance.isPortalGoingToSpawn && scoreTime > (GameManager.Instance.gameLevel * distanceToLevelUp) + distanceToSpawnPortal)
        {
            GameManager.Instance.isPortalGoingToSpawn = true;
            GameManager.Instance.GameLevelUp();
        }
        // Zwi�kszenie poziomu po okre�lonym pokonanym dystansie
        else if (scoreTime > GameManager.Instance.gameLevel * distanceToLevelUp)
        {
            GameManager.Instance.GameLevelUp();
        }

        // Wy�wietlanie uzyskanego wyniku
        scoreText.text = scoreTime.ToString("0");

        // Wy�wietlanie aktualnego poziomu
        levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();

        // Wy�wietlanie ilo�ci z�ota
        goldText.text = "Gold: " + GameManager.Instance.playerGold.ToString();

        // Wy�wietlanie ilo�ci �y�
        livesText.text = "Lives: " + GameManager.Instance.playerLives.ToString();

        // Wy�wietlanie czy gracz posiada tarcze
        shieldText.text = "Shield:  " + GameManager.Instance.playerShield.ToString();
        // =============================================
    }
}
