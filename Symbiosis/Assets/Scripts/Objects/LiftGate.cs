using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftGate : MonoBehaviour
{
    [SerializeField] private float key;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private float distance;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("D" + key))
        {
            Open();
            transform.position += new Vector3(0, distance, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Inventory.instance.HaveKey(key))
        {
            Open();
            GetComponent<AudioSource>().Play();
            StartCoroutine(Openning(distance, 2f));
        }
    }

    IEnumerator Openning(float distance, float duration)
    {
        float timer = 0;
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(0, distance, 0);
        while (timer < duration)
        {
            transform.position = Vector3.Lerp(start, end, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }

    private void Open()
    {
        if (Inventory.instance.HaveKey(key))
            Inventory.instance.RemoveKey(key);
        GetComponent<SpriteRenderer>().sprite = openSprite;
        GetComponent<BoxCollider2D>().enabled = false;
        PlayerPrefs.SetInt("D" + key, 0);
    }
}
