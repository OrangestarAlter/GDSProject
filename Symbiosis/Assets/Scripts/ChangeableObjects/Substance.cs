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
    private Material selfMat;

    public override void OnDisselected()
    {
        selfMat.SetVector("_color", new Vector3(0.0f, 0.0f, 0.0f));
    }

    public override void OnSelected()
    {
        selfMat.SetVector("_color", new Vector3(1.0f, 0.0f, 0.0f));
    }

    protected override void OnChangeDensity(int density)
    {
        if (physicalState != PhysicalState.Solid && solidDensity.Length != 0)
            foreach (int i in solidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Solid;
                    selfMat.SetFloat("_wave", 0f);
                    physics.TurnSolid();
                }
        if (physicalState != PhysicalState.Liquid && liquidDensity.Length != 0)
            foreach (int i in liquidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Liquid;
                    physics.TurnLiquid();
                    selfMat.SetFloat("_wave", 0.1f);
                }
        if (physicalState != PhysicalState.Gas && gasDensity.Length != 0)
            foreach (int i in gasDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Gas;
                    selfMat.SetFloat("_wave", 0f);
                    physics.TurnGas();
                }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        selfMat = transform.GetComponentInChildren<SpriteRenderer>().material;
        if (physicalState == PhysicalState.Liquid)
        {
            selfMat.SetFloat("_wave", 0.1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
