using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDensity : ChangeableObject
{
    [SerializeField] private Vector3[] jumpValues;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

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
                playerController.ChangeJumpValues(jumpValues[0].x, jumpValues[0].y, jumpValues[0].z);
                break;
            case 2:
                playerController.ChangeJumpValues(jumpValues[1].x, jumpValues[1].y, jumpValues[1].z);
                break;
            case 3:
                playerController.ChangeJumpValues(jumpValues[2].x, jumpValues[2].y, jumpValues[2].z);
                break;
            case 4:
                playerController.ChangeJumpValues(jumpValues[3].x, jumpValues[3].y, jumpValues[3].z);
                break;
            case 5:
                playerController.ChangeJumpValues(jumpValues[4].x, jumpValues[4].y, jumpValues[4].z);
                break;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }
}
