using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public TextMeshProUGUI gamePausedText;

    public float rotationSpeed = 100f; // Prêdkoœæ poruszania siê gracza
    private float directionOfMovement; // Decyduje w któr¹ stronê porusza siê gracz (-1 lewo, 1 prawo, 0 do przodu)

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
        // Zatrzymywanie i wznawianie rozgrywki za pomoc¹przycisku "Escape" lub "Space"
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

        // Pobieranie wejœcia ruchu gracza z telefonu (lewa/prawa stron ekranu)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Debug.Log("Klikniêto coœ");
                CheckClickPosition(touch.position);
            }
        }

        // Sprawdzanie kierunku poruszania siê gracza
        DirectionOfPlayerMovement();

        // Oblicz wartoœæ obrotu na podstawie wartoœci osi poziomej
        float rotationAmount = directionOfMovement * rotationSpeed * Time.deltaTime;

        // Obróæ tubê wokó³ osi Z
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
            Debug.Log("Klikniêto obie czêœci ekranu.");
            directionOfMovement = 0f;
        }
        else if (leftClicked)
        {
            Debug.Log("Klikniêto lew¹ czêœæ ekranu.");
            directionOfMovement = 1f;
        }
        else if (rightClicked)
        {
            Debug.Log("Klikniêto praw¹ czêœæ ekranu.");
            directionOfMovement = -1f;
        }
    }
}
