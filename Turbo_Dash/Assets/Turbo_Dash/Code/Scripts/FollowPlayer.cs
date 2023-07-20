using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    private Camera cameraComponent;

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
    private float currentFOV;          // Aktualne pole widzenia kamery
    private float changeTimer = 2f;    // Czas trwania zmiany

    void Start()
    {
        // Pobranie komponentu kamery
        cameraComponent = GetComponent<Camera>();

        // Ustawienie domyœlnego FOV
        currentFOV = defaultFOV;
        cameraComponent.fieldOfView = currentFOV;

        // Ustawienie przesuniêcia kamery wzglêdem gracza
        defaultOffset = new Vector3(0f, 1f, -6f);
        boostOffset = new Vector3(0f, 0.7f, -2f);

        // Ustawienie domyœlnej pozycji kamery
        currentOffset = defaultOffset;
        targetOffset = currentOffset;
        transform.localPosition = player.localPosition + currentOffset;
    }

    void Update()
    {
        if (player != null)
        {
            // Sprawdzanie czy gracz jest w trakcie efektu "Boost" i przypisanie odpowiedniego offsetu
            if (GameManager.Instance.isFOVChanging) StartFOVChange();

            // Interpolujemy wartoœæ pola widzenia za pomoc¹ funkcji Mathf.Lerp i Vector3.Lerp
            if (changeTimer < changeDuration)
            {
                float t = changeTimer / changeDuration;

                currentFOV = Mathf.Lerp(startFOV, targetFOV, t);
                cameraComponent.fieldOfView = currentFOV;

                currentOffset = Vector3.Lerp(startOffset, targetOffset, t);
                transform.localPosition = player.localPosition + currentOffset;

                changeTimer += Time.deltaTime;

                if (changeTimer >= changeDuration)
                {
                    cameraComponent.fieldOfView = targetFOV;
                    transform.localPosition = player.localPosition + targetOffset;
                    changeTimer = changeDuration;
                }
            }
            else
            {
                transform.localPosition = player.localPosition + targetOffset;
            }
        }
    }

    public void StartFOVChange()
    {
        // Zaktualizowanie wszystkich wejœciowych wartoœci dla wykonania p³ynnego przejœcia kamery
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

        GameManager.Instance.isFOVChanging = false;
    }
}
