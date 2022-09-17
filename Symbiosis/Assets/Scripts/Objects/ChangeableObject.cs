using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeableObject : MonoBehaviour
{
    public int density = 0;

    public void ChangeDensity(int i)
    {
        density += i;
        if (density > 2)
            density = 2;
        else if (density < -2)
            density = -2;
        else
            OnChangeDensity(density);
    }

    protected abstract void OnChangeDensity(int density);

    public abstract void OnSelected();

    public abstract void OnDisselected();
}
