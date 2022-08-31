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

    private Material material;

    private void Awake()
    {
        material = physics.GetComponent<SpriteRenderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (physicalState == PhysicalState.Liquid)
        {
            material.SetFloat("_wave", 0.1f);
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
                    material.SetFloat("_wave", 0f);
                }
        if (physicalState != PhysicalState.Liquid && liquidDensity.Length != 0)
            foreach (int i in liquidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Liquid;
                    physics.TurnLiquid();
                    material.SetFloat("_wave", 0.1f);
                }
        if (physicalState != PhysicalState.Gas && gasDensity.Length != 0)
            foreach (int i in gasDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Gas;
                    physics.TurnGas();
                    material.SetFloat("_wave", 0f);
                }
    }
}
