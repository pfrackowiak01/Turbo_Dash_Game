using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f; // Prêdkoœæ poruszania siê gracza
    private float directionOfMovement = 0f; // Decyduje w któr¹ stronê porusza siê gracz (-1 lewo, 1 prawo)

    private Rigidbody playerRigidbody; // Referencja do Rigidbody gracza

    public float forwardForce = 1000f;
    public GameManager gameManager;

    private bool leftClicked;
    private bool rightClicked;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // Pobieranie referencji do Rigidbody z obiektu "Player"
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //playerRigidbody.AddForce(0, 200, 500);
    }

    // Update is called once per frame (to mess with physics)
    private void FixedUpdate()
    {
        leftClicked = false;
        rightClicked = false;
        playerRigidbody.AddForce(0, 0, forwardForce * Time.deltaTime);

        // Pobieranie wejœcia ruchu gracza z telefonu (lewa/prawa stron ekranu)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Debug.Log("Klikniêto coœ");
               CheckClickPosition(touch.position);
            }
        }

        
        // Pobieranie wejœcia ruchu gracza z komputera (strza³ki)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Obliczanie wektora ruchu gracza
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        

        // Sprawdzanie kierunku poruszania siê gracza
        //DirectionOfPlayerMovement();
        // Obliczanie wektora ruchu gracza
        //Vector3 movement = new Vector3(directionOfMovement, 0f, 0f) * moveSpeed * Time.deltaTime;

        // Aktualizacja pozycji gracza
        transform.position += movement;

        // Zakoñczenie gry jeœli gracz wypad³ poza mape
        if (transform.position.y < -2f) gameManager.GameOver();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckClickPosition(Vector2 clickPosition)
    {
        float screenWidth = Screen.width;

        if (clickPosition.x < screenWidth / 2) leftClicked = true;
        else rightClicked = true;
    }

    private void DirectionOfPlayerMovement()
    {
        if (leftClicked && rightClicked || !leftClicked && !rightClicked)
        {
            Debug.Log("Klikniêto obie czêœci ekranu.");
            directionOfMovement = 0f;
        }
        else if (leftClicked)
        {
            Debug.Log("Klikniêto lew¹ czêœæ ekranu.");
            directionOfMovement = -1f;
        }
        else if (rightClicked)
        {
            Debug.Log("Klikniêto praw¹ czêœæ ekranu.");
            directionOfMovement = 1f;
        }
    }
}
