using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float smoothSpeed;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 1, -7);
        smoothSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // Obliczanie docelowej pozycji kamery
        Vector3 desiredPosition = player.position + offset;

        // Interpolacja liniowa pomi�dzy obecn� pozycj� kamery a docelow� pozycj�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition; // Ustaw pozycj� kamery na zinterpolowan� pozycj�
    }
}
