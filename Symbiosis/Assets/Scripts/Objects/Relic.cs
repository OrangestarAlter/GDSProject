using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    public static Relic instance;

    [SerializeField] private GameObject tutorial;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool canPickup = false;

    private void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        else
        {
            tutorial.SetActive(true);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InputController.instance.haveRelic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            InputController.instance.canInput = InputController.instance.haveRelic = true;
            spriteRenderer.sprite = null;
            boxCollider.enabled = false;
            tutorial.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canPickup = false;
    }
}
