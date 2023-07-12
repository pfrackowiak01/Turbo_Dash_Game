using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMovement : MonoBehaviour
{
    private float forwardForce = -1f; // Kierunek poruszania si� rury
    private Vector3 movement;         // Po�o�enie rury

    void FixedUpdate()
    {
        // Obliczanie nowego po�o�enia w kt�rym pojawi si� rura
        movement = new Vector3(0f, 0f, forwardForce) * GameManager.Instance.tubeMoveSpeed * Time.deltaTime;

        // Aktualizacja pozycji rury
        if (!GameManager.Instance.gamePaused) transform.position += movement;

        // Zniszcz rure je�li przekroczy DeadZone
        if (transform.position.z < GameManager.Instance.tubeDeadZone)
        {
            Debug.Log("Tube destroyed");
            Destroy(gameObject);
        }
    }
}
