using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<float> keys = new List<float>();

    private void Awake()
    {
        instance = this;
    }

    public void AddKey(float key, Color color)
    {
        keys.Add(key);
        GameUI.instance.AddKey(color);
    }

    public void RemoveKey(float key)
    {
        for (int i = 0; i < keys.Count; i++)
            if (keys[i] == key)
            {
                keys.RemoveAt(i);
                GameUI.instance.RemoveKey(i);
            }
    }

    public bool HaveKey(float key)
    {
        if (keys.Count > 0)
            foreach (float k in keys)
                if (k == key)
                    return true;
        return false;
    }
}
