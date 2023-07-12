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

    private float boostEffectDuration = 10; // Czas trwania efektu przyspieszenia
    private float lastBoostTime; // Ostatni zapisany czas wzi�cia efektu "Boost"

    private Vector3 destroyedObjectPosition = new Vector3(0,-4,1);

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        // Zako�czenie gry je�li gracz wypad� poza mape
        if (transform.position.y < -10f) LoseGame();
    }


    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.GetComponent<Collider>().tag)
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

            case "Floor":
                Debug.Log("Floor detected.");

                break;

            // ----------------- GEMS -----------------
            case "Boost":
                Debug.Log("Boost gem collected.");

                detectedObject = collision.gameObject;
                BoostEffect();
                break;

            case "Heart":
                Debug.Log("Heart gem collected.");

                detectedObject = collision.gameObject;
                HeartEffect();
                break;

            case "Shield":
                Debug.Log("Shield gem collected.");

                detectedObject = collision.gameObject;
                ShieldEffect();
                break;

            case "Gold":
                Debug.Log("Gold gem collected.");

                detectedObject = collision.gameObject;
                GoldEffect();
                break;

            case "Bonus":
                Debug.Log("Bonus gem collected.");

                detectedObject = collision.gameObject;
                BonusEffect();
                break;

            // -------------- SOMETHING --------------
            default:
                Debug.Log("Something detected?");

                break;
        }
    }

    private void LoseLife()
    {
        // Efekt potrząśnięcia kamery "Shake"
        AnimationManager.Call.CameraShake();

        // Sprawdzenie czy gracz ma tarcze i ją traci
        if (GameManager.Instance.playerShield == true)
        {
            PlayExplosionEffect(destroyedObjectPosition);
            Destroy(detectedObject);
            GameManager.Instance.playerShield = false;
            GameManager.Instance.ToggleVisibilityWithTag("VisualEffectShield");
            Debug.Log("Tarcza zniszczona");
        }
        // Sprawdzenie czy gracz ma życia i traci jedno
        else if (GameManager.Instance.playerLives > 1)
        {
            PlayExplosionEffect(destroyedObjectPosition);
            Destroy(detectedObject);
            GameManager.Instance.playerLives--;
            Debug.Log("Aktualne �ycia: " + GameManager.Instance.playerLives);
        }
        // Jak gracz już nie ma żyć to przegrywa gre
        else
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        PlayExplosionEffect(destroyedObjectPosition);
        Destroy(gameObject);
        GameManager.Instance.playerLives = 0;
        environmentMovement.enabled = false;    //blokowanie poruszania si� na boki
        GameManager.Instance.GameOver();
    }

    private void PlayExplosionEffect(Vector3 position)
    {
        Instantiate(explosion, position, Quaternion.identity);

    }

    // ===========================> GEM EFFECTS <===========================
    private void BoostEffect()
    {
        PlayExplosionEffect(destroyedObjectPosition);
        Destroy(detectedObject);

        // Sprawdzanie, czy efekt przyspieszenia jest ju� aktywny
        if (Time.time - lastBoostTime < boostEffectDuration)
        {
            // Je�li tak, przed�u� czas trwania efektu
            lastBoostTime = Time.time;
        }
        else
        {
            // Je�li nie, uruchom nowy efekt
            GameManager.Instance.boostEffectEnable = true;
            GameManager.Instance.isFOVChanging = true;
            lastBoostTime = Time.time;
        }

        Invoke("TurnOffBoostEffect", boostEffectDuration); // Efekt Boost sam wy��czy si� po okre�lonym czasie
    }

    private void TurnOffBoostEffect()
    {
        // Sprawdzanie, czy nie ma ju� aktywnego efektu przyspieszenia
        if (Time.time - lastBoostTime >= boostEffectDuration)
        {
            GameManager.Instance.boostEffectEnable = false;
            GameManager.Instance.isFOVChanging = true;
        }
    }
    
    private void HeartEffect()
    {
        PlayExplosionEffect(destroyedObjectPosition);
        Destroy(detectedObject);
        if (GameManager.Instance.playerLives < 3) GameManager.Instance.playerLives++;
    }

    private void ShieldEffect()
    {
        PlayExplosionEffect(destroyedObjectPosition);
        Destroy(detectedObject);
        if (GameManager.Instance.playerShield == false)
        {
            GameManager.Instance.playerShield = true;
            GameManager.Instance.ToggleVisibilityWithTag("VisualEffectShield");
        }
        
    }

    private void GoldEffect()
    {
        PlayExplosionEffect(destroyedObjectPosition);
        Destroy(detectedObject);
        GameManager.Instance.playerGold++;
    }

    private void BonusEffect()
    {
        PlayExplosionEffect(destroyedObjectPosition);
        Destroy(detectedObject);
        GameManager.Instance.playerGold = GameManager.Instance.playerGold + 10;
    }
    // =====================================================================
}
