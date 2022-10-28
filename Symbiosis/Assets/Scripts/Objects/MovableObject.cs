using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CircleCollider2D circleCollider;

    private int solidCount = 0;
    private int liquidCount = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void InSolid()
    {
        solidCount++;
        rigid.simulated = false;
        circleCollider.isTrigger = true;
    }

    public void OutSolid()
    {
        if (solidCount != 0)
        {
            solidCount--;
            if (solidCount == 0)
            {
                circleCollider.isTrigger = false;
                rigid.simulated = true;
            }
        }
    }

    public void InLiquid()
    {
        liquidCount++;
        rigid.drag = 10f;
        rigid.angularDrag = 2.5f;
    }

    public void OutLiquid()
    {
        if (liquidCount != 0)
        {
            liquidCount--;
            if (liquidCount == 0)
            {
                rigid.drag = 0;
                rigid.angularDrag = 0.05f;
            }
        }
    }
}
