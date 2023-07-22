using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private GameManager gameManager;

    private float timeScaleBeforePause;

    private void Start()
    {
        gameManager = GameManager.Instance;

        Time.timeScale = 1f;
        timeScaleBeforePause = 1f;

        PauseGame();
    }

    private void Update()
    {
        // ===================> GAME PAUSE SYSTEM <===================
        // Zatrzymywanie i wznawianie rozgrywki za pomoc¹ "Escape", "Space" lub klikniêcia
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            TogglePauseGame();
        }
        // ===========================================================
    }

    public void TogglePauseGame()
    {
        if (gameManager.gamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        gameManager.gamePaused = true;
        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0f;

        // Dodatkowe czynnoœci po zatrzymaniu gry (np. wyœwietlanie menu pauzy)
        // ...
    }

    private void ResumeGame()
    {
        gameManager.gamePaused = false;
        Time.timeScale = timeScaleBeforePause;

        // Dodatkowe czynnoœci po wznowieniu gry
        // ...
    }
}
