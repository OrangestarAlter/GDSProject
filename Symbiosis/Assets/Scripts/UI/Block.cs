using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private int offset = 0;

    private Image image;
    private Text text;
    private Color solidColor = new Color(1f, 0, 0, 0.5f);
    private Color liquidColor = new Color(0, 0, 1f, 0.5f);
    private Color gasColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    private void Awake()
    {
        DensityUI densityUI = GetComponentInParent<DensityUI>();
        densityUI.OnDensityChange += ChangeUI;
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    private void ChangeUI(object sender, DensityUI.OnDensityChangeEventArgs e)
    {
        int density = e.density + offset;
        text.text = density.ToString();
        if (e.solidDensity.Length != 0)
            foreach (int i in e.solidDensity)
                if (i == density)
                {
                    image.color = solidColor;
                }
        if (e.liquidDensity.Length != 0)
            foreach (int i in e.liquidDensity)
                if (i == density)
                {
                    image.color = liquidColor;
                }
        if (e.gasDensity.Length != 0)
            foreach (int i in e.gasDensity)
                if (i == density)
                {
                    image.color = gasColor;
                }
        if (density > 2 || density < -2)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
