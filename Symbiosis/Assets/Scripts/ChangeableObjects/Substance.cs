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

    public override void OnDisselected()
    {
       
    }

    public override void OnSelected()
    {
        
    }

    protected override void OnChangeDensity(int density)
    {
        if (physicalState != PhysicalState.Solid && solidDensity.Length != 0)
            foreach (int i in solidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Solid;
                    physics.TurnSolid();
                }
        if (physicalState != PhysicalState.Liquid && liquidDensity.Length != 0)
            foreach (int i in liquidDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Liquid;
                    physics.TurnLiquid();
                }
        if (physicalState != PhysicalState.Gas && gasDensity.Length != 0)
            foreach (int i in gasDensity)
                if (i == density)
                {
                    physicalState = PhysicalState.Gas;
                    physics.TurnGas();
                }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
