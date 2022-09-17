using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashEffect : MonoBehaviour
{
    private Light2D light2d;

    private void Awake()
    {
        light2d = GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flashing(0.1f, 0.4f, 6f, 3f));
    }

    IEnumerator Flashing(float increaseTime, float decreaseTime, float outerRadius, float intensity)
    {
        light2d.pointLightOuterRadius = 0;
        light2d.intensity = 0;
        float timer = 0;
        while (timer < increaseTime)
        {
            light2d.pointLightOuterRadius = Mathf.Lerp(0, outerRadius, timer / increaseTime);
            light2d.intensity = Mathf.Lerp(0, intensity, timer / increaseTime);
            timer += Time.deltaTime;
            yield return null;
        }
        light2d.pointLightOuterRadius = outerRadius;
        light2d.intensity = intensity;
        timer = 0;
        while (timer < decreaseTime)
        {
            light2d.pointLightOuterRadius = Mathf.Lerp(outerRadius, 0, timer / decreaseTime);
            light2d.intensity = Mathf.Lerp(intensity, 0, timer / decreaseTime);
            timer += Time.deltaTime;
            yield return null;
        }
        light2d.pointLightOuterRadius = 0;
        light2d.intensity = 0;
        Destroy(gameObject);
    }
}
