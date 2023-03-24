using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDispenserPreset : MonoBehaviour
{
    public UIButton button;
    public PaintDispenser dispenser;
    public Color color;
    public Vector3Int m_color;

    bool initialized = false;
    public void Initialize(Vector3Int rgb, Color color)
    {
        this.color = color;
        m_color = rgb;

        button.usePersonalizedColors = true;

        button.personalizedColor1 = color;
        button.personalizedColor2 = color - new Color(0.2f, 0.2f, 0.2f, 0f);

        initialized = true;
    }
    public void SelectPreset()
    {
        if (initialized == false) return;
        
        dispenser.SelectPreset(this);
    }
}
