using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectible")]
public class Collectible : ScriptableObject
{
    public float number;
    public Sprite sprite;
    public string title;
    [TextArea] public string description;
}
