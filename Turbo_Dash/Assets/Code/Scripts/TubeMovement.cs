using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMovement : MonoBehaviour
{
    public float moveSpeed = 50f; // Prêdkoœæ poruszania siê rury

    private float forwardForce = -1f; // kierunek poruszania siê rury
    private Vector3 movement;
    public float deadZone = -60;
    //private Vector3 spawnZone = new Vector3(0f, 0f, 240f);
    //private float tubeLength = 60;

    void Start()
    {

    }

    void FixedUpdate()
    {
        movement = new Vector3(0f, 0f, forwardForce) * moveSpeed * Time.deltaTime;

        // Aktualizacja pozycji rury
        if (!GameManager.Instance.gamePaused) transform.position += movement;

        if (transform.position.z < deadZone)
        {
            //transform.position = spawnZone;
            Debug.Log("Tube destroyed");
            Destroy(gameObject);
        }
    }
}
