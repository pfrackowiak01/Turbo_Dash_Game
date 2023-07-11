using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public Vector3 defaultOffset;
    public Vector3 boostOffset;
    public float smoothSpeed;

    public float startFOV; // Pocz¹tkowe pole widzenia kamery
    public float targetFOV; // Docelowe pole widzenia kamery
    public float defaultFOV = 30f; // Standardowe pole widzenia kamery
    public float boostFOV = 60f; // Na przyspieszeniu pole widzenia kamery
    public float changeDuration = 2f; // Czas trwania zmiany w sekundach
    private float currentFOV; // Aktualne pole widzenia kamery
    private float changeTimer = 2f; // Czas trwania zmiany
    private Camera camera;


    void Start()
    {
        camera = GetComponent<Camera>();
        currentFOV = defaultFOV;
        camera.fieldOfView = currentFOV;

        defaultOffset = new Vector3(0f, 1f, -7f);
        boostOffset = new Vector3(0f, 0.8f, -8f);
        smoothSpeed = 8f;

        offset = defaultOffset;
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

            //  interpolujemy wartoœæ pola widzenia za pomoc¹ funkcji Mathf.Lerp
            if (changeTimer < changeDuration)
            {
                float t = changeTimer / changeDuration;
                currentFOV = Mathf.Lerp(startFOV, targetFOV, t);
                camera.fieldOfView = currentFOV;

                changeTimer += Time.deltaTime;

                if (changeTimer >= changeDuration)
                {
                    camera.fieldOfView = targetFOV;
                    changeTimer = changeDuration;
                }
            }

            // Obliczanie docelowej pozycji kamery
            Vector3 desiredPosition = player.position + offset;

            // Interpolacja liniowa pomiêdzy obecn¹ pozycj¹ kamery a docelow¹ pozycj¹
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition; // Ustaw pozycjê kamery na zinterpolowan¹ pozycjê
        }
    }

    public void StartFOVChange()
    {
        if (GameManager.Instance.boostEffectEnable)
        {
            offset = boostOffset;
            startFOV = defaultFOV;
            targetFOV = boostFOV;
        }
        else
        {
            offset = defaultOffset;
            startFOV = boostFOV;
            targetFOV = defaultFOV;
        }

        changeTimer = 0f;
    }
}
