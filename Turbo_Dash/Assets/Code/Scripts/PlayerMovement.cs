using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f; // Prêdkoœæ poruszania siê gracza

    private Rigidbody playerRigidbody; // Referencja do Rigidbody gracza

    public float forwardForce = 1000f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // Pobieranie referencji do Rigidbody z obiektu "Player"
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerRigidbody.AddForce(0, 200, 500);
    }

    // Update is called once per frame (to mess with physics)
    private void FixedUpdate()
    {

        playerRigidbody.AddForce(0, 0, forwardForce * Time.deltaTime);


        // Pobieranie wejœcia ruchu gracza
        float horizontalInput = Input.GetAxis("Horizontal");

        // Obliczanie wektora ruchu gracza
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;

        // Aktualizacja pozycji gracza
        transform.position += movement;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
