using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public float angle = 45f;

    void Start()
    {
        // Losuje k¹t obrotu kolumny:
        float randomAngle = Random.Range(0, 3) * angle;

        // Obraca obiekt w zale¿noœci od wylosowanego k¹ta
        transform.Rotate(0f, 0f, randomAngle);
    }
}
