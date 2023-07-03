using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                Debug.Log("Wall detected.");
                playerMovement.enabled = false;
                break;

            case "Obstacle":
                Debug.Log("Obstacle detected.");
                playerMovement.enabled = false;
                break;

            case "Floor":
                Debug.Log("Floor detected.");

                break;

            default:
                Debug.Log("Something detected?");
                break;
        }
    }

}
