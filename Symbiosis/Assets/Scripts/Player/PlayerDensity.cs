using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDensity : ChangeableObject
{
    [SerializeField] private Vector3[] jumpValues;
    [SerializeField] private GameObject densityUI;
    [SerializeField] private Text densityText;

    private PlayerController playerController;
    private Material material;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

    public override void OnSelected()
    {
        material.SetVector("_color", new Vector3(1.0f, 0.0f, 0.0f));
        material.SetVector("_leftRight", new Vector2(0.002f, 0.0f));
        material.SetVector("_upDown", new Vector2(0.0f, 0.002f));
        densityUI.SetActive(true);
        densityText.text = density.ToString();
    }

    public override void OnDisselected()
    {
        material.SetVector("_color", new Vector3(0.0f, 0.0f, 0.0f));
        material.SetVector("_leftRight", new Vector2(0.0f, 0.0f));
        material.SetVector("_upDown", new Vector2(0.0f, 0.0f));
        densityUI.SetActive(false);
    }

    protected override void OnChangeDensity(int density)
    {
        switch (density)
        {
            case -2:
                playerController.ChangeJumpValues(jumpValues[0].x, jumpValues[0].y, jumpValues[0].z);
                break;
            case -1:
                playerController.ChangeJumpValues(jumpValues[1].x, jumpValues[1].y, jumpValues[1].z);
                break;
            case 0:
                playerController.ChangeJumpValues(jumpValues[2].x, jumpValues[2].y, jumpValues[2].z);
                break;
            case 1:
                playerController.ChangeJumpValues(jumpValues[3].x, jumpValues[3].y, jumpValues[3].z);
                break;
            case 2:
                playerController.ChangeJumpValues(jumpValues[4].x, jumpValues[4].y, jumpValues[4].z);
                break;
        }
        densityText.text = density.ToString();
    }
}
