using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin channelPerlin;
    private float timer = 0;
    private float duration = 0;
    private float startAmplitude = 0;
    private bool isShaking = false;

    private void Awake()
    {
        instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            channelPerlin.m_AmplitudeGain = Mathf.Lerp(startAmplitude, 0, timer / duration);
            if (timer <= 0)
            {
                channelPerlin.m_AmplitudeGain = 0;
                isShaking = false;
            }
        }
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
    {
        if (!isShaking || (isShaking && time > timer))
        {
            isShaking = true;
            channelPerlin.m_AmplitudeGain = startAmplitude = amplitude;
            channelPerlin.m_FrequencyGain = frequency;
            timer = duration = time;
        }
    }
}
