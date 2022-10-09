using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    public enum ButtonType
    {
        none,
        levels,
        mainMenu,
        settings,
        quit
    }

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Button levelsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject quitPage;

    [SerializeField] private AudioClip confirmClip;
    [SerializeField] private AudioClip cancelClip;

    [SerializeField] private List<GameObject> levelButtons;
    [SerializeField] private List<GameObject> levelCollectibles;

    [SerializeField] private int allLevels;
    [SerializeField] private List<Transform> collectibles;
    [SerializeField] private List<float> allCollectible;
    [SerializeField] private GameObject collectibleUI;

    [SerializeField] private Transform keys;
    [SerializeField] private GameObject keyUI;

    [SerializeField] private GameObject detailPage;
    [SerializeField] private Image detailImage;
    [SerializeField] private Text detailTitle;
    [SerializeField] private Text detailDescription;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Level1"))
            levelsButton.gameObject.SetActive(true);
        for (int i = 0; i < allLevels; i++)
            if (PlayerPrefs.HasKey("Level" + (i + 1)))
            {
                levelButtons[i].SetActive(true);
                levelCollectibles[i].SetActive(true);
            }
        foreach (float collectible in allCollectible)
            if (PlayerPrefs.HasKey("C" + collectible))
                ShowCollectible(collectible);
    }

    public void HideUI()
    {
        gamePanel.SetActive(false);
    }

    public void ShowUI()
    {
        gamePanel.SetActive(true);
    }
    
    public void PauseButton()
    {
        Time.timeScale = 0;
        PlayerController.instance.canMove = false;
        InputController.instance.canInput = false;
        InputController.instance.SetDefultCursor();
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        PlayerController.instance.canMove = true;
        InputController.instance.canInput = true;
        InputController.instance.SetRelicCursor();
        ShowPage(ButtonType.none);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void LevelsButton()
    {
        ShowPage(ButtonType.levels);
    }

    public void MainMenuButton()
    {
        ShowPage(ButtonType.mainMenu);
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
        mainMenuButton.interactable = !(button == ButtonType.mainMenu);
        settingsButton.interactable = !(button == ButtonType.settings);
        quitButton.interactable = !(button == ButtonType.quit);
        levelsPage.SetActive(button == ButtonType.levels);
        mainMenuPage.SetActive(button == ButtonType.mainMenu);
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

    public void LoadLevel(int scene)
    {
        Time.timeScale = 1;
        RespawnPosition.instance.lastScene = 0;
        SceneController.instance.LoadLevel(scene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void AddKey(Color color)
    {
        GameObject keyInstance = Instantiate(keyUI, keys);
        keyInstance.GetComponent<Image>().color = color;
        keyInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f + (keys.childCount - 1) * 75f, -50f);
    }

    public void RemoveKey(int target)
    {
        for (int i = 0; i < keys.childCount; i++)
            if (i > target)
                keys.GetChild(i).GetComponent<RectTransform>().anchoredPosition += new Vector2(-75f, 0);
        Destroy(keys.GetChild(target).gameObject);
    }

    public void ShowCollectible(float collectible)
    {
        int level = Mathf.FloorToInt(collectible);
        int number = Mathf.RoundToInt((collectible - level) * 10);
        collectibles[level - 1].GetChild(number - 1).gameObject.SetActive(true);
        Inventory.instance.collectibleCount++;
    }

    public void ShowPickupUI(Sprite sprite, string title, string description)
    {
        PickupUI UIinstance = Instantiate(collectibleUI, transform).GetComponent<PickupUI>();
        UIinstance.SetPickupUI(sprite, title, description);
    }

    public void ShowDetail(Sprite sprite, string title, string description)
    {
        detailImage.sprite = sprite;
        detailTitle.text = title;
        detailDescription.text = description;
        detailPage.SetActive(true);
    }

    public void CloseDetail()
    {
        detailPage.SetActive(false);
    }
}
