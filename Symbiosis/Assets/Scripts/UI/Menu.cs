using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public enum ButtonType
    {
        none,
        levels,
        newGame,
        settings,
        quit
    }

    [SerializeField] private Button levelsButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject newGamePage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject quitPage;

    [SerializeField] private AudioClip confirmClip;
    [SerializeField] private AudioClip cancelClip;

    [SerializeField] private List<GameObject> levelButtons;
    [SerializeField] private int allLevels;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Level1"))
            levelsButton.gameObject.SetActive(true);
        for (int i = 0; i < allLevels; i++)
            if (PlayerPrefs.HasKey("Level" + i))
                levelButtons[i].SetActive(true);
    }

    public void LevelsButton()
    {
        ShowPage(ButtonType.levels);
    }

    public void NewGameButton()
    {
        ShowPage(ButtonType.newGame);
    }

    public void SettingsButton()
    {
        ShowPage(ButtonType.settings);
    }

    public void QuitButton()
    {
        ShowPage(ButtonType.quit);
    }

    public void BackButton()
    {
        ShowPage(ButtonType.none);
    }

    private void ShowPage(ButtonType button)
    {
        levelsButton.interactable = !(button == ButtonType.levels);
        newGameButton.interactable = !(button == ButtonType.newGame);
        settingsButton.interactable = !(button == ButtonType.settings);
        quitButton.interactable = !(button == ButtonType.quit);
        levelsPage.SetActive(button == ButtonType.levels);
        newGamePage.SetActive(button == ButtonType.newGame);
        settingsPage.SetActive(button == ButtonType.settings);
        quitPage.SetActive(button == ButtonType.quit);
    }

    public void ConfirmSound()
    {
        audioSource.PlayOneShot(confirmClip);
    }

    public void CancelSound()
    {
        audioSource.PlayOneShot(cancelClip);
    }

    public void StartButton()
    {
        PlayerPrefs.DeleteAll();
        if (RelicPickup.instance)
            Destroy(RelicPickup.instance.gameObject);
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
