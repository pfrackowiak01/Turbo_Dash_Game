using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadLootPanel : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI diamondText;
    public int coins;
    public int diamonds;

    private void Start()
    {
        UpdateCoinText();
        UpdateDiamondText();
    }

    public void UpdateCoinText()
    {
        // Pobierz warto�� "Coins" z PlayerPrefs o ile zosta�a wcze�niej zapisana, w przeciwnym razie ustaw na 0
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = coins.ToString();
    }

    public void UpdateDiamondText()
    {
        // Pobierz warto�� "Diamonds" z PlayerPrefs o ile zosta�a wcze�niej zapisana, w przeciwnym razie ustaw na 0
        diamonds = PlayerPrefs.GetInt("Diamonds", 0);
        diamondText.text = diamonds.ToString();
    }

    void Update()
    {
        UpdateCoinText();
        UpdateDiamondText();
    }
}
