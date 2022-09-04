using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public bool key = false;

    private void Awake()
    {
        instance = this;
    }
}
