using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public TextMeshProUGUI gamePausedText;

    public float rotationSpeed = 100f; // Pr�dko�� poruszania si� gracza
    private float directionOfMovement; // Decyduje w kt�r� stron� porusza si� gracz (-1 lewo, 1 prawo, 0 do przodu)

    private bool leftClicked;
    private bool rightClicked;

    Vector3 movement;

    private void Awake()
    {
        
    }

    void Start()
    {
        directionOfMovement = 0f;
    }

    private void FixedUpdate()
    {

    }

    void Update()
    {
        // Zatrzymywanie i wznawianie rozgrywki za pomoc�przycisku "Escape" lub "Space"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.gamePaused) GameManager.Instance.gamePaused = false;
            else GameManager.Instance.gamePaused = true;
            gamePausedText.text = "Game Paused!\n Press \"Space\" or touch the screen\nto continue";
        }

        if (Input.GetKey(KeyCode.LeftArrow)) leftClicked = true;
        else leftClicked = false;
        if (Input.GetKey(KeyCode.RightArrow)) rightClicked = true;
        else rightClicked = false;

        // Pobieranie wej�cia ruchu gracza z telefonu (lewa/prawa stron ekranu)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Debug.Log("Klikni�to co�");
                CheckClickPosition(touch.position);
            }
        }

        // Sprawdzanie kierunku poruszania si� gracza
        DirectionOfPlayerMovement();

        // Oblicz warto�� obrotu na podstawie warto�ci osi poziomej
        float rotationAmount = directionOfMovement * rotationSpeed * Time.deltaTime;

        // Obr�� tub� wok� osi Z
        if (!GameManager.Instance.gamePaused) transform.Rotate(0, 0, rotationAmount);

        //transform.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
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
            Debug.Log("Klikni�to obie cz�ci ekranu.");
            directionOfMovement = 0f;
        }
        else if (leftClicked)
        {
            Debug.Log("Klikni�to lew� cz�� ekranu.");
            directionOfMovement = 1f;
        }
        else if (rightClicked)
        {
            Debug.Log("Klikni�to praw� cz�� ekranu.");
            directionOfMovement = -1f;
        }
    }
}
