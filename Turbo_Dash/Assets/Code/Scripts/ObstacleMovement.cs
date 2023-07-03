using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float obstacleMaxOffset = 2f;
    private float initialPositionX = 0f;
    public float obstacleSpeed = 5f;
    private float obstacleMovement = 1f;
    private bool isMovingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        initialPositionX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Zmiana ruchu na przeciwny jak osi¹gnie maksymalne przesuniêcie
        if (isMovingRight && transform.position.x > initialPositionX + obstacleMaxOffset) 
        {
            isMovingRight = false;
            obstacleMovement = -1;

}
        // Zmiana ruchu na przeciwny jak osi¹gnie maksymalne przesuniêcie
        if (!isMovingRight && transform.position.x < initialPositionX - obstacleMaxOffset) 
        {
            isMovingRight = true;
            obstacleMovement = 1;

        }


        // Obliczanie wektora ruchu przeszkody
        Vector3 movement = new Vector3(obstacleMovement, 0f, 0f) * obstacleSpeed * Time.deltaTime;

        // Aktualizacja pozycji gracza
        transform.position += movement;
    }
}
