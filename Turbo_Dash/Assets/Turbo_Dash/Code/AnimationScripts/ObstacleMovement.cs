using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Check Obstacle Movement")]
    public bool isSideToSide = false;         // Opcja w³¹czaj¹ca/wy³¹czaj¹ca ruch na boki
    public bool isUpAndDown = false;          // Opcja w³¹czaj¹ca/wy³¹czaj¹ca ruch góra-dó³
    public bool isRotating = false;           // Opcja w³¹czaj¹ca/wy³¹czaj¹ca obrót wokó³ w³asnej osi

    [Header("Side-to-Side Movement Parameters")]
    [Tooltip("Maksymalny zasiêg ruchu przeszkody na bok")]
    public float movementRangeSide;
    [Tooltip("Prêdkoœæ ruchu przeszkody")]
    public float movementSpeedSide;

    [Header("Up-and-Down Movement Parameters")]
    [Tooltip("Maksymalny zasiêg ruchu przeszkody w jedn¹ stronê")]
    public float movementRangeUpDown;
    [Tooltip("Prêdkoœæ ruchu przeszkody")]
    public float movementSpeedUpDown;

    [Header("Rotation Parameters")]
    [Tooltip("Prêdkoœæ rotacji przeszkody")]
    public float rotationSpeed = 10f;
    [Tooltip("Oœ rotacji przeszkody")]
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
        // Oblicz now¹ pozycjê poziom¹ w zale¿noœci od czasu
        float horizontalMovement = Mathf.Sin(Time.time * movementSpeedSide) * movementRangeSide;

        // Ustal now¹ pozycjê przeszkody
        Vector3 newPosition = startPosition + new Vector3(horizontalMovement, transform.localPosition.y, transform.localPosition.z);

        // Przesuñ przeszkodê do nowej pozycji
        transform.localPosition = newPosition;
    }

    public void ObstacleUpAndDownMovement()
    {
        // Oblicz now¹ pozycjê poziom¹ w zale¿noœci od czasu
        float verticalMovement = Mathf.Sin(Time.time * movementSpeedUpDown) * movementRangeUpDown;

        // Ustal now¹ pozycjê przeszkody
        Vector3 newPosition = startPosition + new Vector3(transform.localPosition.x, verticalMovement, transform.localPosition.z);

        // Przesuñ przeszkodê do nowej pozycji
        transform.localPosition = newPosition;
    }

    public void ObstacleRotation()
    {
        // Oblicz k¹t rotacji na podstawie czasu i prêdkoœci
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Wykonaj rotacjê obiektu wokó³ okreœlonej osi
        transform.Rotate(rotationAxis, rotationAngle);
    }
}
