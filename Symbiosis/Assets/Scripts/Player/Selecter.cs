using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    [SerializeField] private LayerMask selectLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((selectLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.selectedObjects.Add(changeableObject);
            changeableObject.OnSelected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((selectLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.selectedObjects.Remove(changeableObject);
            changeableObject.OnDisselected();
        }
    }
}
