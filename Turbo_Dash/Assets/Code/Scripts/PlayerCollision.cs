using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public EnvironmentMovement environmentMovement;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.playerLives = 2;
    }

    private void Update()
    {
        // Zakoñczenie gry jeœli gracz wypad³ poza mape
        if (transform.position.y < -10f) LoseGame();
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                Debug.Log("Wall detected.");
                LoseLife();
                break;

            case "Obstacle":
                Debug.Log("Obstacle detected.");
                LoseLife();
                break;

            case "Floor":
                Debug.Log("Floor detected.");

                break;

            default:
                Debug.Log("Something detected?");
                break;
        }
    }

    private void LoseLife()
    {
        if (GameManager.Instance.playerLives > 1)
        {
            GameManager.Instance.playerLives--;
            Debug.Log("Aktualne ¿ycia: " + GameManager.Instance.playerLives);
        }
        else
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        GameManager.Instance.playerLives = 0;
        environmentMovement.enabled = false;
        GameManager.Instance.GameOver();
    }
}
