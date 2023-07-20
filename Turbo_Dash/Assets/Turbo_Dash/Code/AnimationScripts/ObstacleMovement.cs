using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Check Obstacle Movement")]
    public bool isSideToSide = false;         // Opcja w��czaj�ca/wy��czaj�ca ruch na boki
    public bool isUpAndDown = false;          // Opcja w��czaj�ca/wy��czaj�ca ruch g�ra-d�
    public bool isRotating = false;           // Opcja w��czaj�ca/wy��czaj�ca obr�t wok� w�asnej osi

    [Header("Side-to-Side Movement Parameters")]
    [Tooltip("Maksymalny zasi�g ruchu przeszkody na bok")]
    public float movementRangeSide;
    [Tooltip("Pr�dko�� ruchu przeszkody")]
    public float movementSpeedSide;

    [Header("Up-and-Down Movement Parameters")]
    [Tooltip("Maksymalny zasi�g ruchu przeszkody w jedn� stron�")]
    public float movementRangeUpDown;
    [Tooltip("Pr�dko�� ruchu przeszkody")]
    public float movementSpeedUpDown;

    [Header("Rotation Parameters")]
    [Tooltip("Pr�dko�� rotacji przeszkody")]
    public float rotationSpeed = 10f;
    [Tooltip("O� rotacji przeszkody")]
    public Vector3 rotationAxis = Vector3.up;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.gamePaused)
        {
            if (isSideToSide) ObstacleSideToSideMovement();
            if (isUpAndDown) ObstacleUpAndDownMovement();
            if (isRotating) ObstacleRotation();
        }
    }

    public void ObstacleSideToSideMovement()
    {
        // Oblicz now� pozycj� poziom� w zale�no�ci od czasu
        float horizontalMovement = Mathf.Sin(Time.time * movementSpeedSide) * movementRangeSide;

        // Ustal now� pozycj� przeszkody
        Vector3 newPosition = startPosition + new Vector3(horizontalMovement, transform.localPosition.y, transform.localPosition.z);

        // Przesu� przeszkod� do nowej pozycji
        transform.localPosition = newPosition;
    }

    public void ObstacleUpAndDownMovement()
    {
        // Oblicz now� pozycj� poziom� w zale�no�ci od czasu
        float verticalMovement = Mathf.Sin(Time.time * movementSpeedUpDown) * movementRangeUpDown;

        // Ustal now� pozycj� przeszkody
        Vector3 newPosition = startPosition + new Vector3(transform.localPosition.x, verticalMovement, transform.localPosition.z);

        // Przesu� przeszkod� do nowej pozycji
        transform.localPosition = newPosition;
    }

    public void ObstacleRotation()
    {
        // Oblicz k�t rotacji na podstawie czasu i pr�dko�ci
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Wykonaj rotacj� obiektu wok� okre�lonej osi
        transform.Rotate(rotationAxis, rotationAngle);
    }
}
