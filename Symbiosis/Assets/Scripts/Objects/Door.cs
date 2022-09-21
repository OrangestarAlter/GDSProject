using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private float key;
    [SerializeField] private int nextLevel;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject tips;

    private bool isOpen = false;
    private bool isInside = false;

    // Update is called once per frame
    void Update()
    {
        if (isInside && isOpen && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen)
        {
            if (collision.CompareTag("Player") && Inventory.instance.HaveKey(key))
            {
                isOpen = true;
                Inventory.instance.RemoveKey(key);
                GetComponent<SpriteRenderer>().sprite = openSprite;
                isInside = true;
                tips.SetActive(true);
            }
        }
        else
        {
            isInside = true;
            tips.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpen)
        {
            isInside = false;
            tips.SetActive(false);
        }
    }
}
