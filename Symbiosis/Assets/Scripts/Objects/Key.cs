using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float key;

    private bool added = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !added)
        {
            added = true;
            Inventory.instance.AddKey(key, GetComponent<SpriteRenderer>().color);
            Destroy(gameObject);
        }
    }
}
