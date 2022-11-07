using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float key;
    [SerializeField] private int nextScene;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject tips;

    private bool isOpen = false;
    private bool isInside = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("D" + key))
            Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside && isOpen && Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
            SceneController.instance.LoadLevel(nextScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isOpen && Inventory.instance.HaveKey(key))
            {
                Open();
                GetComponent<AudioSource>().Play();
            }
            isInside = true;
            if (isOpen)
                tips.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = false;
            if (isOpen)
                tips.SetActive(false);
        }
    }

    private void Open()
    {
        isOpen = true;
        if (Inventory.instance.HaveKey(key))
            Inventory.instance.RemoveKey(key);
        GetComponent<SpriteRenderer>().sprite = openSprite;
        PlayerPrefs.SetFloat("D" + key, key);
    }
}
