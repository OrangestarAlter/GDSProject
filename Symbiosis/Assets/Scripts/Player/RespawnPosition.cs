using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPosition : MonoBehaviour
{
    public static RespawnPosition instance;
    public int lastScene = 0;
    public bool resetPosition = false;

    private AudioSource audioSource;
    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip levelBGM;

    private void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (lastScene != currentScene)
        {
            if (lastScene == 0 && currentScene != 0)
            {
                audioSource.clip = levelBGM;
                audioSource.Play();
            }
            if ((currentScene == 0 && lastScene != 5 && lastScene != 6) || currentScene == 5 || currentScene == 6)
            {
                audioSource.clip = menuBGM;
                audioSource.Play();
            }
            if (currentScene != 0 && currentScene != 6)
                SetRespawnPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
        else if (resetPosition)
            if (currentScene != 0 && currentScene != 6)
                SetRespawnPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        resetPosition = false;
        lastScene = currentScene;
    }

    public void SetRespawnPosition(Vector3 position)
    {
        transform.position = position;
    }
}
