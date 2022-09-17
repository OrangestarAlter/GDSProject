using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject startPage;
    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject quitPage;

    [SerializeField] private AudioClip confirmClip;
    [SerializeField] private AudioClip cancelClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NewGameButton()
    {
        levelsButton.interactable = true;
        startButton.interactable = false;
        settingsButton.interactable = true;
        quitButton.interactable = true;
        levelsPage.SetActive(false);
        startPage.SetActive(true);
        settingsPage.SetActive(false);
        quitPage.SetActive(false);
    }

    public void LevelsButton()
    {
        levelsButton.interactable = false;
        startButton.interactable = true;
        settingsButton.interactable = true;
        quitButton.interactable = true;
        levelsPage.SetActive(true);
        startPage.SetActive(false);
        settingsPage.SetActive(false);
        quitPage.SetActive(false);
    }

    public void SettingsButton()
    {
        levelsButton.interactable = true;
        startButton.interactable = true;
        settingsButton.interactable = false;
        quitButton.interactable = true;
        levelsPage.SetActive(false);
        startPage.SetActive(false);
        settingsPage.SetActive(true);
        quitPage.SetActive(false);
    }

    public void QuitButton()
    {
        levelsButton.interactable = true;
        startButton.interactable = true;
        settingsButton.interactable = true;
        quitButton.interactable = false;
        levelsPage.SetActive(false);
        startPage.SetActive(false);
        settingsPage.SetActive(false);
        quitPage.SetActive(true);
    }

    public void BackButton()
    {
        levelsButton.interactable = true;
        startButton.interactable = true;
        settingsButton.interactable = true;
        quitButton.interactable = true;
        levelsPage.SetActive(false);
        startPage.SetActive(false);
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

    public void ConfirmSound()
    {
        audioSource.PlayOneShot(confirmClip);
    }

    public void CancelSound()
    {
        audioSource.PlayOneShot(cancelClip);
    }
}
