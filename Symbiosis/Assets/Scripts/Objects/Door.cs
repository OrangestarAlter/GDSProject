using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Inventory.instance.key)
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            tutorial.SetActive(true);
        }
    }
}
