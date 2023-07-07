using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public float moveDistance = 5f;

    void Start()
    {
        // Losuje kierunek (0-3), gdzie:
        // 0 - lewo, 1 - prawo, 2 - g�ra, 3 - d�
        int direction = Random.Range(0, 4);

        // Przesuwa obiekt w zale�no�ci od wylosowanego kierunku
        switch (direction)
        {
            case 0: // Lewo
                transform.Translate(Vector3.left * moveDistance);
                break;
            case 1: // Prawo
                transform.Translate(Vector3.right * moveDistance);
                break;
            case 2: // G�ra
                transform.Translate(Vector3.up * moveDistance);
                break;
            case 3: // D�
                transform.Translate(Vector3.down * moveDistance);
                break;
        }

    }
}
