using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DensityUI : MonoBehaviour
{
    public event EventHandler<OnDensityChangeEventArgs> OnDensityChange;
    public class OnDensityChangeEventArgs : EventArgs
    {
        public int density;
        public int[] solidDensity;
        public int[] liquidDensity;
        public int[] gasDensity;
    }

    private int density = 0;

    public void SetDensityUI(int d, int[] solidDensity, int[] liquidDensity, int[] gasDensity)
    {
        density = d;
        OnDensityChange?.Invoke(this, new OnDensityChangeEventArgs { 
            density = density,
            solidDensity = solidDensity,
            liquidDensity = liquidDensity,
            gasDensity = gasDensity });
    }
}
