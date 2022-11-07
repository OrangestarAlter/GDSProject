using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
            Pickup(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !added)
            Pickup(0.5f);
    }

    private void Pickup(float time)
    {
        added = true;
        Inventory.instance.AddKey(key, GetComponent<SpriteRenderer>().color);
        PlayerPrefs.SetFloat("K" + key, key);
        if (time !=0)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Light2D>().enabled = false;
        }
        Destroy(gameObject, time);
    }
}
