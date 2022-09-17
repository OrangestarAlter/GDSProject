using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SavePoint : MonoBehaviour
{
    private Light2D light2d;
    private AudioSource audioSource;

    private bool isActive = false;
    private float timer = 0;
    private int sign = 1;

    private void Awake()
    {
        light2d = GetComponent<Light2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!isActive)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 1f)
            {
                sign *= -1;
                timer = 0;
            }
            light2d.intensity += sign * 0.25f * Time.fixedDeltaTime;
            light2d.pointLightOuterRadius += sign * 5f * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnPosition.instance.SetRespawnPosition(transform.position);
            SetActiveLight();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("RespawnPosition"))
        {
            SetBreathLight();
        }
    }

    private void SetActiveLight()
    {
        if (!isActive)
        {
            isActive = true;
            light2d.color = new Color(1f, 0, 1f);
            light2d.pointLightInnerRadius = 1f;
            light2d.pointLightOuterRadius = 3f;
            light2d.intensity = 2f;
            audioSource.Play();
        }
    }

    private void SetBreathLight()
    {
        isActive = false;
        light2d.color = new Color(1f, 0.75f, 0);
        light2d.pointLightInnerRadius = 0;
        light2d.pointLightOuterRadius = 1f;
        light2d.intensity = 0.5f;
        timer = 0;
        sign = 1;
    }
}
