using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    public Transform player;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pobranie przebytego dystansu i wy�wietlenie jej jako liczby ca�kowitej na ekranie
        scoreText.text = player.position.z.ToString("0");
    }
}
