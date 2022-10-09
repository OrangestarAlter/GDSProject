using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float key;

    private bool added = false;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.instance.keyCount++;
        if (PlayerPrefs.HasKey("D" + key))
            Destroy(gameObject);
        else if (PlayerPrefs.HasKey("K" + key))
            Pickup();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !added)
            Pickup();
    }

    private void Pickup()
    {
        added = true;
        Inventory.instance.AddKey(key, GetComponent<SpriteRenderer>().color);
        PlayerPrefs.SetFloat("K" + key, key);
        Destroy(gameObject);
    }
}
