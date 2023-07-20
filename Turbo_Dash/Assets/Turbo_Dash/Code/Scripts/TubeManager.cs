using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TubeManager : MonoBehaviour
{
    private GameManager gameManager;
    public Transform parrent;

    public Texture tubeInsideTexture;
    public Texture tubeOutsideTexture;

    private int percentageChance;  // Procentowa szansa na pojawienie siê przeszkód
    private float level;           // Aktualny level rozgrywanej gry

    private void Start()
    {
        gameManager = GameManager.Instance;

        SetTexture();

        // Pobierz i zapisz aktualny level gry
        level = gameManager.gameLevel;

        // Dostosowanie poziomu trudnoœci leveli przez zwiêkszenie prawdopodobieñstwa wystêpowania przeszkód
        if (level < 3) percentageChance = 30;
        else if (level < 5) percentageChance = 40;
        else if (level < 7) percentageChance = 50;
        else percentageChance = 60;

        // System obs³uguj¹cy spawnowanie obiektów w rurze (pod warunkiem, ¿e zespawni³y siê ju¿ wszystkie bezpieczne/puste rury)
        if (gameManager.safeTubes == 0)
        {
            if (Random.Range(1, 100) <= percentageChance)
            {
                // Stwórz prefab przeszkody pobrany z Obstacle ScriptableObject
                gameManager.SpawnObject(gameManager.usedObstacles, parrent);
            }
            else
            {
                // Stwórz prefab œciany pobrany z Wall ScriptableObject
                gameManager.SpawnObject(gameManager.usedWalls, parrent);
            }

            // Stwórz Portal do Space levelu, je¿eli jest to zamierzane
            if (gameManager.isPortalGoingToSpawn)
            {
                gameManager.SpawnPortal(parrent);
                gameManager.isPortalGoingToSpawn = false;
            }
            // Stwórz ScriptableObject gemu (range 1 i 3 to szansa 1/3 czyli 33%)
            else if (Random.Range(1, 3) == 1) gameManager.SpawnObject(gameManager.usedGems, parrent);
        }
        else GameManager.Instance.safeTubes--;
    }

    private void SetTexture()
    {
        // Pobierz wszystkie komponenty Renderer w obiektach potomnych
        Renderer[] renderers = GetComponentsInChildren<Renderer>(); 

        if (renderers != null)
        {
            foreach (Renderer renderer in renderers)
            {
                // Przypisz now¹ teksturê do w³aœciwoœci mainTexture w zale¿noœci od lokacji
                if (gameManager.gameLocation == GameManager.Location.Inside)
                {
                    renderer.material = GameManager.Instance.gameTheme.TubeInside;
                }
                else
                {
                    renderer.material = GameManager.Instance.gameTheme.TubeOutside;
                }
            }
        }
    }

}
