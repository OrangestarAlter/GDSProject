using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RelicPickup : MonoBehaviour
{
    public static RelicPickup instance;

    [SerializeField] private GameObject tutorial;

    private bool canPickup = false;
    private Light2D light2d;
    private float timer = 0;
    private int sign = 1;

    private void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            light2d = GetComponent<Light2D>();
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
        Cursor.visible = false;
        InputController.instance.haveRelic = false;
        GameUI.instance.HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            Cursor.visible = true;
            InputController.instance.SetRelicCursor();
            InputController.instance.canInput = InputController.instance.haveRelic = true;
            GameUI.instance.ShowUI();
            tutorial.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= 2f)
        {
            sign *= -1;
            timer = 0;
        }
        light2d.intensity += sign * 0.25f * Time.fixedDeltaTime;
        light2d.pointLightOuterRadius += sign * 5f * Time.fixedDeltaTime;
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
