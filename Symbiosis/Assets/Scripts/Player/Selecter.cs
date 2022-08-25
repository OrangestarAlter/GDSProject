using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ChangeableObject") || collision.CompareTag("Player"))
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.selectedObjects.Add(changeableObject);
            changeableObject.OnSelected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ChangeableObject") || collision.CompareTag("Player"))
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.selectedObjects.Remove(changeableObject);
            changeableObject.OnDisselected();
        }
    }
}
