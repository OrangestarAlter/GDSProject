using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private Dialogue ending1;
    [SerializeField] private Dialogue ending2;
    [SerializeField] private GameObject CG1;
    [SerializeField] private GameObject CG2;

    [SerializeField] private Image whiteEffect;
    [SerializeField] private CGDialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        InputController.instance.haveRelic = false;
        InputController.instance.SetDefultCursor();
        StartCoroutine(EndDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndDialogue()
    {
        float timer = 0;
        while (timer < 30f)
        {
            Time.timeScale = Mathf.Lerp(1f, 10f, timer / 30f);
            if (timer >= 15f)
            {
                whiteEffect.color = new Color(1f, 1f, 1f, Mathf.Lerp(0, 1f, (timer - 15f) / 15f));
            }
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Time.timeScale = 1f;
        whiteEffect.color = new Color(1f, 1f, 1f, 0);
        PlayerController.instance.gameObject.SetActive(false);
        if (Inventory.instance.haveAllCollectible)
        {
            dialogue.ShowDialogue(ending2);
            CG2.SetActive(true);
        }
        else
        {
            dialogue.ShowDialogue(ending1);
            CG1.SetActive(true);
        }
    }
}
