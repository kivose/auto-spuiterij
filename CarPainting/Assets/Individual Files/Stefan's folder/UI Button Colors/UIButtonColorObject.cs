using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UI Button Color Module", menuName = "UI Button Color Module")]
public class UIButtonColorObject : ScriptableObject
{
    public float colorLerpSpeed;

    [Header("Default")]
    public Color defaultColor1;
    public Color defaultColor2;

    [Header("Hovered")]
    public Color hoveredColor1;
    public Color hoveredColor2;

    [Header("Selected")]
    public Color selectedColor1;
    public Color selectedColor2;

    [Header("Clicked")]
    public Color clickedColor1;
    public Color clickedColor2;


}
