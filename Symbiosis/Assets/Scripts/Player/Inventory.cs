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

    public void AddKey(float key)
    {
        keys.Add(key);
    }

    public void RemoveKey(float key)
    {
        keys.Remove(key);
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
