using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public TextMeshProUGUI gamePausedText;

    private float rotationSpeed = 10f; // Prêdkoœæ poruszania siê gracza
    private float directionOfMovement; // Decyduje w któr¹ stronê porusza siê gracz (-1 lewo, 1 prawo, 0 do przodu)
    private float screenWidth;         // Szerokoœæ ekranu

    private bool leftClicked;
    private bool rightClicked;

    private Quaternion initialRotation;

    void Start()
    {
        // Pobiera szerokoœæ ekranu
        screenWidth = Screen.width;

        // Ustawia pocz¹tkowy kierunek obrotu;
        directionOfMovement = 0f;

        // W³¹cza ¿yroskop
        Input.gyro.enabled = true;

        // Ustala pocz¹tkow¹ rotacjê telefonu jako referencjê
        initialRotation = Input.gyro.attitude;
    }

    void Update()
    {
        // ===================> GAME PAUSE SYSTEM <===================
        // Zatrzymywanie i wznawianie rozgrywki za pomoc¹ "Escape" lub "Space"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (GameManager.Instance.gamePaused) GameManager.Instance.gamePaused = false;
            else GameManager.Instance.gamePaused = true;
            gamePausedText.text = "Game Paused!\n Press \"Space\" or touch the screen\nto continue";
            initialRotation = Input.gyro.attitude;
        }
        // ===========================================================


        // ===================> STEROWANIE RÊCZNE <===================
        // Reset flag kierunków
        leftClicked = false;
        rightClicked = false;

        // Ustawienie flagi kierunków za pomoc¹ wciœniêtych strza³ek
        SetFlagsByKeyboard();

        // Ustawienie flagi kierunków za pomoc¹ doktniêæ ekranu
        SetFlagsByTouchscreen();

        // Obliczenie wartoœci obrotu z ustawionych flag i wykonanie go
        PlayerMovementByDirections();
        // -----------------------------------------------------------


        // =================> STEROWANIE ¯YROSKOPEM <=================
        // Obliczenie wartoœci obrotu ze ¿yroskopu i wykonanie go
        PlayerMovementByGyroscope();
        // -----------------------------------------------------------
    }

    private void SetFlagsByKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) leftClicked = true;
        if (Input.GetKey(KeyCode.RightArrow)) rightClicked = true;
    }

    private void SetFlagsByTouchscreen()
    {
        // Pobieranie wejœcia ruchu gracza z telefonu (lewa/prawa stron ekranu)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
        {
            // Dla ka¿dego klikniêcia/doktniêcia sprawdza pozycje (czy by³o po lewej czy po prawej)
            foreach (Touch touch in Input.touches)
            {
                // Sprawdza pozycje klikniêcia na ekranie (lewo/prawo) i zaznacza odpowiedni¹ flagê
                if (touch.position.x < screenWidth / 2) leftClicked = true;
                else rightClicked = true;
            }
        }
    }

    private void PlayerMovementByDirections()
    {
        if (leftClicked && rightClicked || !leftClicked && !rightClicked)
        {
            Debug.Log("Klikniêto obie czêœci ekranu LUB nic.");
            directionOfMovement = 0f;
        }
        else if (leftClicked)
        {
            Debug.Log("Klikniêto lew¹ czêœæ ekranu.");
            directionOfMovement = 10f;
        }
        else if (rightClicked)
        {
            Debug.Log("Klikniêto praw¹ czêœæ ekranu.");
            directionOfMovement = -10f;
        }

        // Oblicz wartoœæ obrotu na podstawie wartoœci osi poziomej
        float rotationAmount = directionOfMovement * rotationSpeed * Time.deltaTime;

        // Obróæ rurê o obliczon¹ wartoœæ wokó³ osi Z
        if (!GameManager.Instance.gamePaused) transform.Rotate(0, 0, rotationAmount);
    }

    private void PlayerMovementByGyroscope()
    {
        // Pobierz wartoœæ przechylenia telefonu
        Quaternion currentRotation = Quaternion.Inverse(initialRotation) * Input.gyro.attitude;

        // Ogranicz przechylenie do osi Z (lewo/prawo)
        float tiltAngle = Mathf.Clamp(currentRotation.z * Mathf.Rad2Deg, -20f, 20f);

        // Oblicz k¹t obrotu na podstawie przechylenia
        float rotationAmount = tiltAngle * rotationSpeed * Time.deltaTime;

        // Obróæ rurê o obliczon¹ wartoœæ wokó³ osi Z
        if (!GameManager.Instance.gamePaused) transform.Rotate(0, 0, rotationAmount);
    }
}
