using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject startPage;
    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject quitPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NewGameButton()
    {
        levelsPage.SetActive(false);
        settingsPage.SetActive(false);
        quitPage.SetActive(false);
        startPage.SetActive(true);
    }

    public void LevelsButton()
    {
        startPage.SetActive(false);
        settingsPage.SetActive(false);
        quitPage.SetActive(false);
        levelsPage.SetActive(true);
    }

    public void SettingsButton()
    {
        startPage.SetActive(false);
        levelsPage.SetActive(false);
        quitPage.SetActive(false);
        settingsPage.SetActive(true);
    }

    public void QuitButton()
    {
        startPage.SetActive(false);
        levelsPage.SetActive(false);
        settingsPage.SetActive(false);
        quitPage.SetActive(true);
    }

    public void BackButton()
    {
        startPage.SetActive(false);
        levelsPage.SetActive(false);
        settingsPage.SetActive(false);
        quitPage.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
