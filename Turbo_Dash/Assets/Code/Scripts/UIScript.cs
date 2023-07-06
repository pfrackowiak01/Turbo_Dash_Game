using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public GameObject gamePausedScreen;
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
        if(!GameManager.Instance.gamePaused)
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

        // Zwi�kszanie poziomu po okre�lonym czasie i jego wy�wietlanie
        if (timerLevel > levelTime)
        {
            GameManager.Instance.GameLevelUp();
            levelText.text = "Level: " + GameManager.Instance.gameLevel.ToString();
            timerLevel = 0f;
        }

        // Wy�wietlanie uzyskanego wyniku
        scoreText.text = scoreTime.ToString("0");
    }
}
