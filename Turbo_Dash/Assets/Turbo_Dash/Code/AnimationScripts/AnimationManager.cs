using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // ===================== SINGLETON =====================
    // Deklaruje w�a�ciwo�� statyczn� o nazwie "Instance".
    public static AnimationManager Call { get; private set; }

    // Sprawdza, czy ju� istnieje instancja AnimationManager.
    // Je�li nie, ustawia warto�� w�a�ciwo�ci "Instance" na bie��c� instancj� (this).
    // Mo�emy si� do niej odwo�ywa� przez "AnimationManager.Call".
    private void Awake()
    {
        // Obiekt nie zostanie zniszczony podczas przej�cia mi�dzy scenami.
        if (Call == null)
        {
            Call = this;
            DontDestroyOnLoad(gameObject);
        }
        // Niszczy nowo utworzony obiekt AnimationManager, aby zachowa� tylko jedn� instancje w grze.
        else Destroy(gameObject);
    }
    // =====================================================


    // ---------------------- GLOBALS ----------------------
    private Animator cameraFollow;


    // ----------------- GLOBAL FUNCTIONS ------------------
    public void Presets()
    {
        cameraFollow = GameObject.Find("CameraFollow").GetComponent<Animator>();

    }

    public void CameraShake()
    {
        cameraFollow = GameObject.Find("CameraFollow").GetComponent<Animator>();
        cameraFollow.SetTrigger("shake");
    }
}
