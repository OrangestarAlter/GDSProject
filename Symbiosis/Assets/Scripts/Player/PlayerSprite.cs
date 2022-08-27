using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Vector3[] shakeValue;
    
    private PlayerDensity density;

    private void Awake()
    {
        density = GetComponentInParent<PlayerDensity>();
    }

    public void Shake()
    {
        switch (density.density)
        {
            case 1:
                CameraController.instance.ShakeCamera(shakeValue[0].x, shakeValue[0].y, shakeValue[0].z);
                break;
            case 2:
                CameraController.instance.ShakeCamera(shakeValue[1].x, shakeValue[1].y, shakeValue[1].z);
                break;
        }
    }
}
