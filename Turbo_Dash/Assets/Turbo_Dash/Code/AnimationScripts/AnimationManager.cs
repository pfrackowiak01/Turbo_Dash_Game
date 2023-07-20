using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // ===================== SINGLETON =====================
    // Deklaruje w³aœciwoœæ statyczn¹ o nazwie "Instance".
    public static AnimationManager Call { get; private set; }

    // Sprawdza, czy ju¿ istnieje instancja AnimationManager.
    // Jeœli nie, ustawia wartoœæ w³aœciwoœci "Instance" na bie¿¹c¹ instancjê (this).
    // Mo¿emy siê do niej odwo³ywaæ przez "AnimationManager.Call".
    private void Awake()
    {
        // Obiekt nie zostanie zniszczony podczas przejœcia miêdzy scenami.
        if (Call == null)
        {
            Call = this;
            DontDestroyOnLoad(gameObject);
        }
        // Niszczy nowo utworzony obiekt AnimationManager, aby zachowaæ tylko jedn¹ instancje w grze.
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
