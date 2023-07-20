using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    public TextMeshProUGUI gamePausedText;

    public float rotationSpeed = 10f;     // Pr�dko�� poruszania si� gracza na boki
    public float maxRotationSpeed = 20f;  // Maxymalna pr�dko�� poruszania si� gracza na boki

    private float directionOfMovement;    // Decyduje w kt�r� stron� porusza si� gracz (-1 lewo, 1 prawo, 0 do przodu)
    private float screenWidth;            // Szeroko�� ekranu

    private bool leftClicked;
    private bool rightClicked;

    private Quaternion initialRotation;

    void Start()
    {
        // Pobiera szeroko�� ekranu
        screenWidth = Screen.width;

        // Ustawia pocz�tkowy kierunek obrotu;
        directionOfMovement = 0f;

        // W��cza �yroskop
        Input.gyro.enabled = true;

        // Ustala pocz�tkow� rotacj� telefonu jako referencj�
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


        // ===================> STEROWANIE R�CZNE <===================
        // Reset flag kierunk�w
        leftClicked = false;
        rightClicked = false;

        // Ustawienie flagi kierunk�w za pomoc� wci�ni�tych strza�ek
        SetFlagsByKeyboard();

        // Ustawienie flagi kierunk�w za pomoc� doktni�� ekranu
        SetFlagsByTouchscreen();

        // Obliczenie warto�ci obrotu z ustawionych flag i wykonanie go
        PlayerMovementByDirections();
        // -----------------------------------------------------------


        // =================> STEROWANIE �YROSKOPEM <=================
        // Obliczenie warto�ci obrotu ze �yroskopu i wykonanie go
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
        // Pobieranie wej�cia ruchu gracza z telefonu (lewa/prawa stron ekranu)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
        {
            // Dla ka�dego klikni�cia/doktni�cia sprawdza pozycje (czy by�o po lewej czy po prawej)
            foreach (Touch touch in Input.touches)
            {
                // Sprawdza pozycje klikni�cia na ekranie (lewo/prawo) i zaznacza odpowiedni� flag�
                if (touch.position.x < screenWidth / 2) leftClicked = true;
                else rightClicked = true;
            }
        }
    }

    private void PlayerMovementByDirections()
    {
        // Ustawienie odpowiednich flag obrotu po klikni�ciach
        if (leftClicked && rightClicked || !leftClicked && !rightClicked)
        {
            Debug.Log("Klikni�to obie cz�ci ekranu LUB nic.");
            directionOfMovement = 0f;
        }
        else if (leftClicked)
        {
            Debug.Log("Klikni�to lew� cz�� ekranu.");
            directionOfMovement = rotationSpeed;
        }
        else if (rightClicked)
        {
            Debug.Log("Klikni�to praw� cz�� ekranu.");
            directionOfMovement = -rotationSpeed;
        }

        // Oblicz warto�� obrotu na podstawie warto�ci osi poziomej
        GameManager.Instance.rotationAmount = directionOfMovement * rotationSpeed * Time.deltaTime;

        // Wykonanie odpowiedniego obrotu
        MakeTheProperRotate();
    }

    private void PlayerMovementByGyroscope()
    {
        // Pobierz warto�� przechylenia telefonu
        Quaternion currentRotation = Quaternion.Inverse(initialRotation) * Input.gyro.attitude;

        // Ogranicz przechylenie do osi Z (lewo/prawo)
        float tiltAngle = Mathf.Clamp(currentRotation.z * Mathf.Rad2Deg, -maxRotationSpeed, maxRotationSpeed);

        // Oblicz k�t obrotu na podstawie przechylenia
        GameManager.Instance.rotationAmount = tiltAngle * rotationSpeed * Time.deltaTime;

        // Wykonanie odpowiedniego obrotu
        MakeTheProperRotate();
    }

    private void MakeTheProperRotate()
    {
        // Sprawd� czy gra NIE jest zapauzowana
        if (!GameManager.Instance.gamePaused)
        {
            // Sprawd� w kt�rej lokacji znajduje si� gracz
            if (GameManager.Instance.gameLocation == GameManager.Location.Inside)
            {
                // Obr�� rur� o obliczon� warto�� wok� osi Z dla gracza w �rodku rury
                transform.Rotate(0, 0, GameManager.Instance.rotationAmount);
            }
            else
            {
                // Obr�� rur� o obliczon� warto�� wok� osi Z dla gracza na zewn�trz rury
                transform.Rotate(0, 0, -GameManager.Instance.rotationAmount);
            }
        }
    }
}
