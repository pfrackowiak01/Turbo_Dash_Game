using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMovement : MonoBehaviour
{
    private float forwardForce = -1f; // Kierunek poruszania siê rury
    private Vector3 movement;         // Po³o¿enie rury

    void FixedUpdate()
    {
        // Obliczanie nowego po³o¿enia w którym pojawi siê rura
        movement = new Vector3(0f, 0f, forwardForce) * GameManager.Instance.tubeMoveSpeed * Time.deltaTime;

        // Aktualizacja pozycji rury
        if (!GameManager.Instance.gamePaused) transform.position += movement;

        // Zniszcz rure jeœli przekroczy DeadZone
        if (transform.position.z < GameManager.Instance.tubeDeadZone)
        {
            Debug.Log("Tube destroyed");
            Destroy(gameObject);
        }
    }
}
