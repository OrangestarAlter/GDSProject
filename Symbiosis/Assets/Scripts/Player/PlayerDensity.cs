using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDensity : ChangeableObject
{
    private SpriteRenderer spriteRenderer;

    public override void OnDisselected()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }

    public override void OnSelected()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }

    protected override void OnChangeDensity(int density)
    {
        switch (density)
        {
            case 1:
                spriteRenderer.color = Color.red;
                break;
            case 2:
                spriteRenderer.color = Color.yellow;
                break;
            case 3:
                spriteRenderer.color = Color.green;
                break;
            case 4:
                spriteRenderer.color = Color.blue;
                break;
            case 5:
                spriteRenderer.color = Color.magenta;
                break;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
