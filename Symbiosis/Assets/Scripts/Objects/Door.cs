using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float key;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Inventory.instance.HaveKey(key))
        {
            Inventory.instance.RemoveKey(key);
            GetComponent<SpriteRenderer>().sprite = openSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            if (tutorial)
                tutorial.SetActive(true);
        }
    }
}
