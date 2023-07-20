using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalityEffect : MonoBehaviour
{
    private Renderer renderer;
    private bool isImmortal = false;
    private float effectDuration;
    private float elapsedTime = 0f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }

    void Update()
    {
        if (GameManager.Instance.boostEffectEnable) effectDuration = 11f;
        else effectDuration = 3f;

        if (GameManager.Instance.playerImmortality || GameManager.Instance.boostEffectEnable)
        {
            if (!isImmortal)
            {
                isImmortal = true;
                elapsedTime = 0f;
                renderer.enabled = true;
            }

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= effectDuration)
            {
                renderer.enabled = false;
                isImmortal = false;
                GameManager.Instance.playerImmortality = false;
            }
        }
        else if (isImmortal)
        {
            renderer.enabled = false;
            isImmortal = false;
            GameManager.Instance.playerImmortality = false;
        }
    }
}
