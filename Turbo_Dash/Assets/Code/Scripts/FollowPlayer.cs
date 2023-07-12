using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    private Camera camera;

    public Vector3 startOffset;
    public Vector3 targetOffset;
    public Vector3 defaultOffset;
    public Vector3 boostOffset;
    public Vector3 currentOffset;

    private float startFOV;            // Pocz¹tkowe pole widzenia kamery
    private float targetFOV;           // Docelowe pole widzenia kamery
    private float defaultFOV = 40f;    // Standardowe pole widzenia kamery
    private float boostFOV = 90f;      // Na przyspieszeniu pole widzenia kamery
    private float changeDuration = 2f; // Czas trwania zmiany w sekundach
    private float currentFOV;         // Aktualne pole widzenia kamery
    private float changeTimer = 2f;   // Czas trwania zmiany

    void Start()
    {
        camera = GetComponent<Camera>();
        currentFOV = defaultFOV;
        camera.fieldOfView = currentFOV;

        defaultOffset = player.position + new Vector3(0f, 1f, -6f);
        boostOffset = player.position + new Vector3(0f, 0.7f, -2f);
        currentOffset = defaultOffset;

        transform.position = currentOffset;
    }

    void Update()
    {
        if (player != null)
        {
            // Sprawdzanie czy gracz jest w trakcie efektu "Boost" i przypisanie odpowiedniego offsetu
            if (GameManager.Instance.isFOVChanging)
            {
                StartFOVChange();
                GameManager.Instance.isFOVChanging = false;
            }

            // Interpolujemy wartoœæ pola widzenia za pomoc¹ funkcji Mathf.Lerp i Vector3.Lerp
            if (changeTimer < changeDuration)
            {
                float t = changeTimer / changeDuration;

                currentFOV = Mathf.Lerp(startFOV, targetFOV, t);
                camera.fieldOfView = currentFOV;

                currentOffset = Vector3.Lerp(startOffset, targetOffset, t);
                transform.position = currentOffset;

                if(!GameManager.Instance.gamePaused) changeTimer += Time.deltaTime;

                if (changeTimer >= changeDuration)
                {
                    camera.fieldOfView = targetFOV;
                    transform.position = targetOffset;
                    changeTimer = changeDuration;
                }
            }

            // Obliczenie wychylenie kamery przy poruszaniu gracza
            //offset.x = GameManager.Instance.rotationAmount;

            // Obliczanie docelowej pozycji kamery
            //Vector3 desiredPosition = player.position + offset;

            // Interpolacja liniowa pomiêdzy obecn¹ pozycj¹ kamery a docelow¹ pozycj¹
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }

    public void StartFOVChange()
    {
        if (GameManager.Instance.boostEffectEnable)
        {
            startOffset = defaultOffset;
            targetOffset = boostOffset;
            startFOV = defaultFOV;
            targetFOV = boostFOV;
        }
        else
        {
            startOffset = boostOffset;
            targetOffset = defaultOffset;
            startFOV = boostFOV;
            targetFOV = defaultFOV;
        }

        changeTimer = 0f;
    }
}
