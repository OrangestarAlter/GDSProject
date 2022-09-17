using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Vector3[] shakeValue;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private AudioClip[] landClips;

    private AudioSource audioSource;
    private PlayerDensity density;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        density = GetComponentInParent<PlayerDensity>();
    }

    public void Jump()
    {
        switch (density.density)
        {
            case -2:
                audioSource.PlayOneShot(jumpClips[0]);
                break;
            case -1:
                audioSource.PlayOneShot(jumpClips[1]);
                break;
            case 0:
                audioSource.PlayOneShot(jumpClips[2]);
                break;
            case 1:
                audioSource.PlayOneShot(jumpClips[3]);
                break;
            case 2:
                audioSource.PlayOneShot(jumpClips[4]);
                break;
        }
    }

    public void Land()
    {
        switch (density.density)
        {
            case 1:
                CameraController.instance.ShakeCamera(shakeValue[1].x, shakeValue[1].y, shakeValue[1].z);
                audioSource.PlayOneShot(landClips[0]);
                break;
            case 2:
                CameraController.instance.ShakeCamera(shakeValue[0].x, shakeValue[0].y, shakeValue[0].z);
                audioSource.PlayOneShot(landClips[1]);
                break;
        }
    }

    public void BreakTerrain()
    {
        if (density.density > 0)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.parent.position, Vector2.down, Mathf.Infinity, hitLayer);
            if (raycastHit)
                if (raycastHit.transform.CompareTag("Breakable"))
                    raycastHit.transform.GetComponent<BreakableObject>().Break(density.density);
        }
    }
}
