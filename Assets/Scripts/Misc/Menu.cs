using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject continueButton;
    public Toggle easyModeToggle;

    static bool firstSceneLoad = true;

    public bool resetSaveOnPlay = true;

    private void Start()
    {

        if (firstSceneLoad && resetSaveOnPlay)
            SaveLoadUtils.ResetSave();

        firstSceneLoad = false;

        if (!SaveLoadUtils.hasSave && continueButton)
            continueButton.SetActive(false);
    }

    public void PlayGame()
    {
        int easyMode = easyModeToggle.isOn ? 1 : 0;
        SaveLoadUtils.GenerateNewSave(easyMode);
        SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        SaveLoadUtils.SetEasyMode(easyModeToggle.isOn ? 1 : 0);
        switch (SaveLoadUtils.GetCurrentLevel())
        {
            case 0: SceneManager.LoadScene("Level1"); break;
            case 1: SceneManager.LoadScene("Level2"); break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
