using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameManager gameManager;
    public int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                Debug.Log("Wall detected.");
                LooseLife();
                break;

            case "Obstacle":
                Debug.Log("Obstacle detected.");
                LooseLife();
                break;

            case "Floor":
                Debug.Log("Floor detected.");

                break;

            default:
                Debug.Log("Something detected?");
                break;
        }
    }

    private void LooseLife()
    {
        if (playerLives > 1)
        {
            playerLives--;
            Debug.Log("Aktualne ¿ycia: " + playerLives);
        }
        else LooseGame();
    }

    private void LooseGame()
    {
        playerMovement.enabled = false;
        FindObjectOfType<GameManager>().GameOver();
    }
}
