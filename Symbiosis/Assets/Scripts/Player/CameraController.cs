using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin channelPerlin;
    private CinemachineConfiner confiner;

    private Transform target;
    private float timer = 0;
    private float duration = 0;
    private float startAmplitude = 0;
    private bool isShaking = false;

    private void Awake()
    {
        instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        confiner = GetComponent<CinemachineConfiner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
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

    public void SetCameraRange(PolygonCollider2D polygonCollider)
    {
        confiner.m_BoundingShape2D = polygonCollider;
    }

    public void OnTargetObjectWarped(Vector3 positionDelta)
    {
        virtualCamera.OnTargetObjectWarped(target, positionDelta);
    }
}
