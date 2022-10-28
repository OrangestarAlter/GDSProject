using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private List<float> allCollectible;
    private List<float> keys = new List<float>();
    public int keyCount = 0;
    public int collectibleCount = 0;
    public bool haveAllCollectible = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (float collectible in allCollectible)
            if (PlayerPrefs.HasKey("C" + collectible))
            {
                if (GameUI.instance)
                    GameUI.instance.ShowCollectible(collectible);
                collectibleCount++;
            }
        if (collectibleCount == allCollectible.Count)
            haveAllCollectible = true;
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

    public void GetCollectible(float collectible, Sprite sprite, string title, string description)
    {
        collectibleCount++;
        GameUI.instance.ShowCollectible(collectible);
        GameUI.instance.ShowPickupUI(sprite, title, description);
    }
}
