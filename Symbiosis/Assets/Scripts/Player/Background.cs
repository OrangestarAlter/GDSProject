using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private Vector2 followSpeed;
    private Vector3 lastCameraPos;
    private float textureSize;

    // Start is called before the first frame update
    void Start()
    {
        lastCameraPos = cameraTrans.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureSize = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 temp = cameraTrans.position - lastCameraPos;
        transform.position += new Vector3(temp.x * followSpeed.x, temp.y * followSpeed.y, 0);
        lastCameraPos = cameraTrans.position;

        if (Mathf.Abs(cameraTrans.position.x - transform.position.x) >= textureSize)
        {
            float offset = (cameraTrans.position.x - transform.position.x) % textureSize;
            transform.position = new Vector3(cameraTrans.position.x + offset, transform.position.y);
        }
    }
}
