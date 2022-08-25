using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    public List<ChangeableObject> selectedObjects = new List<ChangeableObject>();
    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private GameObject selecter;
    [SerializeField] private float selectRange;

    private GameObject selecterInstance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        LeftClick();
        RightClick();
        SelectDensity();
    }

    private void LeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickLayer);
            if (raycastHit)
            {
                ClearSelect();
                selecterInstance = Instantiate(selecter, raycastHit.transform);
                selecterInstance.transform.localScale = new Vector3(selectRange, selectRange, 1f);
                ChangeableObject changeableObject = raycastHit.transform.GetComponent<ChangeableObject>();
                selectedObjects.Add(changeableObject);
                changeableObject.OnSelected();
            }
        }
    }

    private void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClearSelect();
        }
    }

    private void ClearSelect()
    {
        if (selecterInstance)
            Destroy(selecterInstance);
        if(selectedObjects.Count != 0)
        {
            foreach (ChangeableObject obj in selectedObjects)
                obj.OnDisselected();
            selectedObjects.Clear();
        }
    }

    private void SelectDensity()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeDensity(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeDensity(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeDensity(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeDensity(4);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeDensity(5);
    }

    private void ChangeDensity(int density)
    {
        if (selectedObjects.Count != 0)
        {
            foreach (ChangeableObject obj in selectedObjects)
                obj.ChangeDensity(density);
            ClearSelect();
        }
    }
}
