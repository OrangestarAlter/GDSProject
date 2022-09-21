using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private GameObject selecter;
    [SerializeField] private float selectRange;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private GameObject flashEffect;

    public bool canInput = false;
    public bool haveRelic = true;
    private List<ChangeableObject> selectedObjects = new List<ChangeableObject>();
    private GameObject selecterInstance;
    private bool selfSelected = false;
    private Vector2 cursorHotspot;
    private float timer = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
        SetRelicCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            if (Input.GetKeyDown(KeyCode.S))
                if (!selfSelected)
                    Select(transform);
                else
                    ClearSelect();
            LeftClick();
            RightClick();
            SelectDensity();
        }
    }

    private void LeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickLayer);
            if (raycastHit)
                Select(raycastHit.transform);
        }
    }

    private void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClearSelect();
        }
    }

    private void Select(Transform selected)
    {
        ClearSelect();
        if (selected == transform)
            selfSelected = true;
        selecterInstance = Instantiate(selecter, selected);
        selecterInstance.transform.localScale = new Vector3(selectRange, selectRange, 1f);
        ChangeableObject changeableObject = selected.transform.GetComponent<ChangeableObject>();
        selectedObjects.Add(changeableObject);
        changeableObject.OnSelected();
    }

    private void ClearSelect()
    {
        if (selecterInstance)
            Destroy(selecterInstance);
        if (selectedObjects.Count != 0)
        {
            foreach (ChangeableObject obj in selectedObjects)
                obj.OnDisselected();
            selectedObjects.Clear();
        }
        selfSelected = false;
    }

    private void SelectDensity()
    {
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                ChangeDensity(1);
                timer = 0;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                ChangeDensity(-1);
                timer = 0;
            }
        }
    }

    private void ChangeDensity(int i)
    {
        if (selectedObjects.Count != 0)
        {
            foreach (ChangeableObject obj in selectedObjects)
                obj.ChangeDensity(i);
            Instantiate(flashEffect, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
    }

    public void AddSelected(ChangeableObject obj)
    {
        if (!selectedObjects.Contains(obj))
            selectedObjects.Add(obj);
    }

    public void RemoveSelected(ChangeableObject obj)
    {
        if (selectedObjects.Contains(obj))
            selectedObjects.Remove(obj);
    }

    public void SetRelicCursor()
    {
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public void SetDefultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
