using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterEffect : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        image.color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(Enter(0.5f));
    }

    IEnumerator Enter(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            image.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0, timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(1f, 1f, 1f, 0);
        PlayerController.instance.canMove = true;
        InputController.instance.canInput = InputController.instance.haveRelic;
    }
}
