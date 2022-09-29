using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] private int currentLevel;

    private void Awake()
    {
        instance = this;
        PlayerPrefs.SetInt("Level" + currentLevel, currentLevel);
    }

    public void LoadLevel(int scene)
    {
        if (Inventory.instance.keyCount != 0)
            for (int i = 1; i <= Inventory.instance.keyCount; i++)
            {
                PlayerPrefs.DeleteKey("K" + (currentLevel + i * 0.1f));
                PlayerPrefs.DeleteKey("D" + (currentLevel + i * 0.1f));
            }
        SceneManager.LoadScene(scene);
    }
}
