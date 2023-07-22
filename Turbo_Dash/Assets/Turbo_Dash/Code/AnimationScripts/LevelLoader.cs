using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public int menuSceneIndex = 0;
    public int gameplaySceneIndex = 1;
    public int shopSceneIndex = 2;
    public int customizationSceneIndex = 3;
    public int skillTreeSceneIndex = 4;

    void Update()
    {
        
    }

    public void PlayButtonClicked()
    {
        StartCoroutine(LoadLevel(gameplaySceneIndex));
    }

    public void ShopButtonClicked()
    {
        StartCoroutine(LoadLevel(shopSceneIndex));
    }

    public void CustomizationButtonClicked()
    {
        StartCoroutine(LoadLevel(customizationSceneIndex));
    }

    public void MenuButtonClicked()
    {
        StartCoroutine(LoadLevel(menuSceneIndex));
    }

    public void SkillTreeButtonClicked()
    {
        StartCoroutine(LoadLevel(skillTreeSceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Animacja zanikania
        transition.SetTrigger("StartCrossfade");

        // Zaczekanie
        yield return new WaitForSeconds(transitionTime);

        // £adowanie Sceny
        SceneManager.LoadScene(levelIndex);
    }

}
