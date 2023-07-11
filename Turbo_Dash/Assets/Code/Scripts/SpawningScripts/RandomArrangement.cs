using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomArrangement : MonoBehaviour
{
    private float angle = 45f;

    void Start()
    {
        // Losuje k�t obrotu kolumny: 
        float randomAngle = Random.Range(0, 7) * angle;

        // Obraca obiekt w zale�no�ci od wylosowanego k�ta
        transform.Rotate(0f, 0f, randomAngle);
    }
}
