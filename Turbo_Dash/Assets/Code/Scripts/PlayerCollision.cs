using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = 3;
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
        if (playerLives > 1)
        {
            playerLives--;
            Debug.Log("Aktualne ¿ycia: " + playerLives);
        }
        else LoseGame();
    }

    private void LoseGame()
    {
        playerMovement.enabled = false;
        GameManager.Instance.GameOver();
    }
}
