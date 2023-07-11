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

            // Upływ czasu
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

        // ==========> EKRAN ZAKOŃCZONEJ GRY <==========
        if (!GameManager.Instance.gameHasEnded)
        {
            // Ukrycie widoku zakończonej gry
            gameOverScreen.SetActive(false);
        }
        else
        {
            // Pokazanie widoku zakończonej gry
            gameOverScreen.SetActive(true);
        }

        // ==========> EKRAN ROZGRYWANEJ GRY <==========

        // Zwiększanie poziomu po określonym czasie i jego wyświetlanie
        if (timerLevel > levelTime)
        {
            GameManager.Instance.GameLevelUp();
            levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();
            timerLevel = 0f;
        }

        // Wyświetlanie uzyskanego wyniku
        scoreText.text = scoreTime.ToString("0");

        // Wyświetlanie ilości złota
        goldText.text = "Gold: " + GameManager.Instance.playerGold.ToString();

        // Wyświetlanie ilości żyć
        livesText.text = "Lives: " + GameManager.Instance.playerLives.ToString();

        // Wyświetlanie czy gracz posiada tarcze
        shieldText.text = "Shield:  " + GameManager.Instance.playerShield.ToString();
    }
}
