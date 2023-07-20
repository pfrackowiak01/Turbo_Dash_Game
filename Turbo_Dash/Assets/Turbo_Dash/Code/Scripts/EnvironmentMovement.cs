using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public TextMeshProUGUI gamePausedText;

    public float rotationSpeed = 10f;     // Prêdkoœæ poruszania siê gracza na boki
    public float maxRotationSpeed = 20f;  // Maxymalna prêdkoœæ poruszania siê gracza na boki

    private float directionOfMovement;    // Decyduje w któr¹ stronê porusza siê gracz (-1 lewo, 1 prawo, 0 do przodu)
    private float screenWidth;            // Szerokoœæ ekranu

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
        // ====================> GAME PAUSE VIEW <====================
        if (GameManager.Instance.gamePaused)
        {
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
        // Ustawienie odpowiednich flag obrotu po klikniêciach
        if (leftClicked && rightClicked || !leftClicked && !rightClicked)
        {
            Debug.Log("Klikniêto obie czêœci ekranu LUB nic.");
            directionOfMovement = 0f;
        }
        else if (leftClicked)
        {
            Debug.Log("Klikniêto lew¹ czêœæ ekranu.");
            directionOfMovement = rotationSpeed;
        }
        else if (rightClicked)
        {
            Debug.Log("Klikniêto praw¹ czêœæ ekranu.");
            directionOfMovement = -rotationSpeed;
        }

        // Oblicz wartoœæ obrotu na podstawie wartoœci osi poziomej
        GameManager.Instance.rotationAmount = directionOfMovement * rotationSpeed * Time.deltaTime;

        // Wykonanie odpowiedniego obrotu
        MakeTheProperRotate();
    }

    private void PlayerMovementByGyroscope()
    {
        // Pobierz wartoœæ przechylenia telefonu
        Quaternion currentRotation = Quaternion.Inverse(initialRotation) * Input.gyro.attitude;

        // Ogranicz przechylenie do osi Z (lewo/prawo)
        float tiltAngle = Mathf.Clamp(currentRotation.z * Mathf.Rad2Deg, -maxRotationSpeed, maxRotationSpeed);

        // Oblicz k¹t obrotu na podstawie przechylenia
        GameManager.Instance.rotationAmount = tiltAngle * rotationSpeed * Time.deltaTime;

        // Wykonanie odpowiedniego obrotu
        MakeTheProperRotate();
    }

    private void MakeTheProperRotate()
    {
        // SprawdŸ czy gra NIE jest zapauzowana
        if (!GameManager.Instance.gamePaused)
        {
            // SprawdŸ w której lokacji znajduje siê gracz
            if (GameManager.Instance.gameLocation == GameManager.Location.Inside)
            {
                // Obróæ rurê o obliczon¹ wartoœæ wokó³ osi Z dla gracza w œrodku rury
                transform.Rotate(0, 0, GameManager.Instance.rotationAmount);
            }
            else
            {
                // Obróæ rurê o obliczon¹ wartoœæ wokó³ osi Z dla gracza na zewn¹trz rury
                transform.Rotate(0, 0, -GameManager.Instance.rotationAmount);
            }
        }
    }
}
