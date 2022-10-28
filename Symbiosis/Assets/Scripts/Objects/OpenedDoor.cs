using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedDoor : MonoBehaviour
{
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private GameObject tips;

    private bool isInside = false;

    // Update is called once per frame
    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0 && PlayerController.instance.canMove)
        {
            Transform player = PlayerController.instance.transform;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector3 positionDelta = newPosition - player.position;
            player.position = newPosition;
            CameraController.instance.OnTargetObjectWarped(positionDelta);
            GameUI.instance.DoorEffect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = true;
            tips.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = false;
            tips.SetActive(false);
        }
    }
}
