using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPosition : MonoBehaviour
{
    public static RespawnPosition instance;
    public int lastScene = 0;

    private void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
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
        if (currentScene != 0 && lastScene != currentScene)
            SetRespawnPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        lastScene = currentScene;
    }

    public void SetRespawnPosition(Vector3 position)
    {
        transform.position = position;
    }
}
