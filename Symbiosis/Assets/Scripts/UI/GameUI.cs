using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [SerializeField] Transform keys;
    [SerializeField] GameObject keyUI;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddKey(Color color)
    {
        GameObject keyInstance = Instantiate(keyUI, keys);
        keyInstance.GetComponent<Image>().color = color;
        keyInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f + (keys.childCount - 1) * 75f, -50f);
    }

    public void RemoveKey(int target)
    {
        for (int i = 0; i < keys.childCount; i++)
            if (i > target)
                keys.GetChild(i).GetComponent<RectTransform>().anchoredPosition += new Vector2(-75f, 0);
        Destroy(keys.GetChild(target).gameObject);
    }
}
