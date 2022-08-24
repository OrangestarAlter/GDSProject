using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ChangeableObject"))
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.selectedObjects.Add(changeableObject);
            changeableObject.OnSelected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ChangeableObject"))
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.selectedObjects.Remove(changeableObject);
            changeableObject.OnDisselected();
        }
    }
}
