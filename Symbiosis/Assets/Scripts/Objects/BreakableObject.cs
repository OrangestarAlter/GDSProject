using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public int breakLevel = 1;

    public void Break(int density)
    {
        if (density <= breakLevel)
        {
            CameraController.instance.ShakeCamera(5f, 5f, 0.5f);
            Destroy(gameObject);
        }
    }
}
