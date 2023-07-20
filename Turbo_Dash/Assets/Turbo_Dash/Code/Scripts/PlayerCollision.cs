using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerCollision : MonoBehaviour
{
    public EnvironmentMovement environmentMovement;
    public GameObject explosion;
    private GameObject detectedObject;
    private GameManager gameManager;
    private Renderer playerRenderer;


    private float boostEffectDuration = 10f; // Czas trwania efektu przyspieszenia
    private float lastBoostTime;             // Ostatni zapisany czas wziecia efektu "Boost"

    private Vector3 destroyedObjectPositionInside = new Vector3(0f,-4f,1f);
    private Vector3 destroyedObjectPositionOutside = new Vector3(0f, 6.5f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        lastBoostTime = Time.time - boostEffectDuration;

        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material = GameManager.Instance.gameTheme.Player;

    }

    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B)) BoostEffect();
        if (Input.GetKeyDown(KeyCode.S)) ShieldEffect();
        if (Input.GetKeyDown(KeyCode.H)) HeartEffect();
        if (Input.GetKeyDown(KeyCode.G)) GoldEffect();
        if (Input.GetKeyDown(KeyCode.P)) DiamondEffect();
        if (Input.GetKeyDown(KeyCode.L)) gameManager.GameLevelUp();
        if (Input.GetKeyDown(KeyCode.O)) gameManager.playerLives = 1000;
#endif
        // Zarządzanie pozycją gracza, gdy jest w środku rury, czy na zewnątrz (Space level)
        if (gameManager.gameLocation == GameManager.Location.Inside) transform.localPosition = new Vector3(0f, -4f, 0f);
        else transform.localPosition = new Vector3(0f, 6.5f, 0f);
    }


    private void OnTriggerEnter(Collider collision)
    {
        String inputValueTag = collision.GetComponent<Collider>().tag;

        switch (inputValueTag)
        {
            // -------------- STRUCTURES --------------
            case "Wall":
                Debug.Log("Wall detected.");

                detectedObject = collision.gameObject;
                LoseLife();
                break;

            case "Obstacle":
                Debug.Log("Obstacle detected.");

                detectedObject = collision.gameObject;
                LoseLife();
                break;

            case "Portal":
                Debug.Log("Portal detected.");
                detectedObject = collision.gameObject;
                if (gameManager.gameLocation == GameManager.Location.Inside)
                    gameManager.gameLocation = GameManager.Location.Outside;
                else gameManager.gameLocation = GameManager.Location.Inside;
                break;

            case "Floor":
                Debug.Log("Floor detected.");

                break;

            // ----------------- GEMS -----------------
            case "Boost":
                Debug.Log("Boost gem collected.");
                
                detectedObject = collision.gameObject;
                DestroyDetectedObject();
                BoostEffect();
                break;

            case "Heart":
                Debug.Log("Heart gem collected.");
                
                detectedObject = collision.gameObject;
                DestroyDetectedObject();
                HeartEffect();
                break;

            case "Shield":
                Debug.Log("Shield gem collected.");
                
                detectedObject = collision.gameObject;
                DestroyDetectedObject();
                ShieldEffect();
                break;

            case "Gold":
                Debug.Log("Gold gem collected.");
                
                detectedObject = collision.gameObject;
                DestroyDetectedObject();
                GoldEffect();
                break;

            case "Diamond":
                Debug.Log("Diamond gem collected.");
                
                detectedObject = collision.gameObject;
                DestroyDetectedObject();
                DiamondEffect();
                break;

            // -------------- SOMETHING --------------
            default:
                Debug.Log("Something detected?");

                break;
        }
    }

    private void LoseLife()
    {
        // Sprawdzanie czy gracz jest niemiertelny
        if (gameManager.playerImmortality == true || gameManager.boostEffectEnable == true)
        {
            Debug.Log("NIEŚMIERTELNOŚĆ!");
        }
        // Sprawdzenie czy gracz ma tarcze i ją traci
        else if(gameManager.playerShield == true)
        {
            DestroyDetectedObject();
            gameManager.playerShield = false;
            gameManager.ToggleVisibilityWithTag("VisualEffectShield");
            Debug.Log("Tarcza zniszczona");
        }
        // Sprawdzenie czy gracz ma życia i traci jedno
        else if (gameManager.playerLives > 1)
        {
            PlayExplosionEffect();
            DestroyDetectedObject();
            ImmortalityEffect();
            gameManager.playerLives--;
            Debug.Log("Aktualne �ycia: " + gameManager.playerLives);
        }
        // Jak gracz już nie ma żyć to przegrywa gre
        else
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        PlayExplosionEffect();
        Destroy(gameObject);
        gameManager.playerLives = 0;
        environmentMovement.enabled = false;    //blokowanie poruszania się na boki
        gameManager.GameOver();
    }

    private void DestroyDetectedObject()
    {
        // Zapamiętaj rodzica wykrytego obiektu
        Transform parent = detectedObject.transform.parent;

        // Sprawdź tag rodzica wykrytego obiektu
        if (parent != null)
        {
            // Jeśli jest przeszkodą to usuń rodzica, a jeśli nie to usuń dziecko
            if (parent.tag == "Obstacle") Destroy(parent.gameObject);
            else Destroy(detectedObject);
        }
    }

    private void PlayExplosionEffect()
    {
        // Efekt potrząśnięcia kamery "Shake"
        AnimationManager.Call.CameraShake();
        Handheld.Vibrate();

        // Stwórz eksplozje w odpowiednim miejscu
        if (gameManager.gameLocation == GameManager.Location.Inside)
        Instantiate(explosion, destroyedObjectPositionInside, Quaternion.identity);
        else Instantiate(explosion, destroyedObjectPositionOutside, Quaternion.identity);
    }

    // ===========================> GEM EFFECTS <===========================
    private void BoostEffect()
    {
        // Sprawdzanie, czy efekt przyspieszenia jest już aktywny
        if (gameManager.boostEffectEnable == true)
        {
            // Jezeli tak, przedluz czas trwania efektu
            lastBoostTime = Time.time;
        }
        else
        {
            // Jezeli nie, uruchom nowy efekt
            gameManager.boostEffectEnable = true;
            gameManager.isFOVChanging = true;
            lastBoostTime = Time.time;
            ImmortalityEffect();
        }

        Invoke("TurnOffBoostEffect", boostEffectDuration); // Efekt Boost sam wyłączy się po określonym czasie
    }

    private void TurnOffBoostEffect()
    {
        // Sprawdzanie, czy nie ma ju� aktywnego efektu przyspieszenia
        if (Time.time - lastBoostTime >= boostEffectDuration)
        {
            gameManager.boostEffectEnable = false;
            gameManager.isFOVChanging = true;
        }
    }
    
    private void HeartEffect()
    {
        if (gameManager.playerLives < 3) gameManager.playerLives++;
    }

    private void ShieldEffect()
    {
        if (gameManager.playerShield == false)
        {
            gameManager.playerShield = true;
            gameManager.ToggleVisibilityWithTag("VisualEffectShield");
        }
    }

    private void GoldEffect()
    {
        gameManager.playerGold++;
    }

    private void DiamondEffect()
    {
        gameManager.playerGold = gameManager.playerGold + 10;
    }

    private void ImmortalityEffect()
    {
        gameManager.playerImmortality = true;
    }
    // =====================================================================
}
