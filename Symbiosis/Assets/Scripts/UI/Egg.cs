using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private Dialogue ending3;
    [SerializeField] private CGDialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        dialogue.ShowDialogue(ending3);
    }
}
