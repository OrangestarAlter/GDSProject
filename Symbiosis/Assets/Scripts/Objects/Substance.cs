using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substance : ChangeableObject
{
    public enum PhysicalState
    {
        Solid,
        Liquid,
        Gas
    }

    [SerializeField] private PhysicalState physicalState;
    [SerializeField] private int[] solidDensity;
    [SerializeField] private int[] liquidDensity;
    [SerializeField] private int[] gasDensity;
    [SerializeField] private Physics physics;

    [SerializeField] private Sprite solidSprite;
    [SerializeField] private Sprite liquidSprite;
    [SerializeField] private Sprite gasSprite;

    private SpriteRenderer spriteRenderer;
    private Material material;

    private void Awake()
    {
        spriteRenderer = physics.GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (physicalState)
        {
            case PhysicalState.Solid:
                spriteRenderer.sprite = solidSprite;
                material.SetFloat("_alpha", 0.9f);
                break;
            case PhysicalState.Liquid:
                spriteRenderer.sprite = liquidSprite;
                material.SetFloat("_alpha", 0.6f);
                material.SetFloat("_wave", 0.1f);
                break;
            case PhysicalState.Gas:
                spriteRenderer.sprite = gasSprite;
                material.SetFloat("_alpha", 0.3f);
                material.SetFloat("_isFog", 1f);
                break;
        }
    }

    public override void OnSelected()
    {
        material.SetVector("_color", new Vector3(1.0f, 0.0f, 0.0f));
    }

    public override void OnDisselected()
    {
        material.SetVector("_color", new Vector3(0.0f, 0.0f, 0.0f));
    }

    protected override void OnChangeDensity(int density)
    {
        if (physicalState != PhysicalState.Solid && solidDensity.Length != 0)
            foreach (int i in solidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Solid;
                    physics.TurnSolid();
                    spriteRenderer.sprite = solidSprite;
                    material.SetFloat("_alpha", 0.9f);
                    material.SetFloat("_wave", 0f);
                    material.SetFloat("_isFog", 0f);
                }
        if (physicalState != PhysicalState.Liquid && liquidDensity.Length != 0)
            foreach (int i in liquidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Liquid;
                    physics.TurnLiquid();
                    spriteRenderer.sprite = liquidSprite;
                    material.SetFloat("_alpha", 0.6f);
                    material.SetFloat("_wave", 0.1f);
                    material.SetFloat("_isFog", 0f);
                }
        if (physicalState != PhysicalState.Gas && gasDensity.Length != 0)
            foreach (int i in gasDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Gas;
                    physics.TurnGas();
                    spriteRenderer.sprite = gasSprite;
                    material.SetFloat("_alpha", 0.3f);
                    material.SetFloat("_wave", 0f);
                    material.SetFloat("_isFog", 1f);
                }
    }

    private void SetColorAlpha(float alpha)
    {
        Color motoColor = spriteRenderer.color;
        spriteRenderer.color = new Color(motoColor.r, motoColor.g, motoColor.b, alpha);
    }
}
