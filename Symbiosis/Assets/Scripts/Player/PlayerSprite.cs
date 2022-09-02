using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Vector3[] shakeValue;
    [SerializeField] private LayerMask hitLayer;
    
    private PlayerDensity density;

    private void Awake()
    {
        density = GetComponentInParent<PlayerDensity>();
    }

    public void Shake()
    {
        switch (density.density)
        {
            case 5:
                CameraController.instance.ShakeCamera(shakeValue[0].x, shakeValue[0].y, shakeValue[0].z);
                break;
            case 4:
                CameraController.instance.ShakeCamera(shakeValue[1].x, shakeValue[1].y, shakeValue[1].z);
                break;
        }
    }

    public void BreakTerrain()
    {
        if (density.density >= 4)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.parent.position, Vector2.down, Mathf.Infinity, hitLayer);
            if (raycastHit)
                if (raycastHit.transform.CompareTag("Breakable"))
                    raycastHit.transform.GetComponent<BreakableObject>().Break(density.density);
        }
    }
}
