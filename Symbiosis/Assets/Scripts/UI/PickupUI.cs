using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text title;
    [SerializeField] private Text description;

    private bool canContinue = false;

    // Update is called once per frame
    void Update()
    {
        if (canContinue && Input.anyKeyDown)
        {
            canContinue = false;
            GetComponent<Animator>().Play("ClosePickup");
        }
    }

    public void SetPickupUI(Sprite image, string title, string description)
    {
        if (image)
            this.image.sprite = image;
        if (title != null)
            this.title.text = title;
        if (description != null)
            this.description.text = description;
        Time.timeScale = 0;
        PlayerController.instance.canMove = false;
        InputController.instance.canInput = false;
        Cursor.visible = false;
        GameUI.instance.HideUI();
    }

    public void Open()
    {
        canContinue = true;
    }

    public void Close()
    {
        Time.timeScale = 1;
        PlayerController.instance.canMove = true;
        InputController.instance.canInput = true;
        Cursor.visible = true;
        GameUI.instance.ShowUI();
        Destroy(gameObject);
    }
}
