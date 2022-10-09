using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CameraController.instance.SetCameraRange(GetComponent<PolygonCollider2D>());
    }
}
