using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private Vector2 followSpeed;
    private Vector3 lastCameraPos;
    private float textureSizeX;
    private float textureSizeY;

    // Start is called before the first frame update
    void Start()
    {
        lastCameraPos = cameraTrans.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureSizeX = sprite.texture.width / sprite.pixelsPerUnit;
        textureSizeY = sprite.texture.height / sprite.pixelsPerUnit;
    }

    private void Update()
    {
        Vector3 temp = cameraTrans.position - lastCameraPos;
        transform.position += new Vector3(temp.x * followSpeed.x, temp.y * followSpeed.y, 0);
        lastCameraPos = cameraTrans.position;

        if (Mathf.Abs(cameraTrans.position.x - transform.position.x) >= textureSizeX)
        {
            float offset = (cameraTrans.position.x - transform.position.x) % textureSizeX;
            transform.position = new Vector3(cameraTrans.position.x + offset, transform.position.y);
        }
        if (Mathf.Abs(cameraTrans.position.y - transform.position.y) >= textureSizeY)
        {
            float offset = (cameraTrans.position.y - transform.position.y) % textureSizeY;
            transform.position = new Vector3(transform.position.x, cameraTrans.position.y + offset);
        }
    }
}
