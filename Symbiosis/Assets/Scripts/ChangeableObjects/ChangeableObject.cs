using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeableObject : MonoBehaviour
{
    public int density;

    public void ChangeDensity(int d)
    {
        density = d;
        OnChangeDensity(density);
    }

    protected abstract void OnChangeDensity(int density);

    public abstract void OnSelected();

    public abstract void OnDisselected();
}
