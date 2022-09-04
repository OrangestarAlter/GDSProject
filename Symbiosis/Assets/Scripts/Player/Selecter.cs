using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    [SerializeField] private LayerMask selectLayer;

    private Material material;
    private float time;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        material.SetFloat("_Speed", time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((selectLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.AddSelected(changeableObject);
            changeableObject.OnSelected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((selectLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            ChangeableObject changeableObject = collision.GetComponent<ChangeableObject>();
            InputController.instance.RemoveSelected(changeableObject);
            changeableObject.OnDisselected();
        }
    }
}
