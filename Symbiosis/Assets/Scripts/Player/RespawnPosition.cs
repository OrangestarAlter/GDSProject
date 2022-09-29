using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPosition : MonoBehaviour
{
    public static RespawnPosition instance;
    private int lastSceneIndex = 0;

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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != 0 && lastSceneIndex != currentSceneIndex)
        {
            lastSceneIndex = currentSceneIndex;
            SetRespawnPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }

    public void SetRespawnPosition(Vector3 position)
    {
        transform.position = position;
    }
}
