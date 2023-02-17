using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Car Part", menuName = "Car Part")]
public class CarPartObject : ScriptableObject
{
    [Header("Main Car Part data")]
    [Tooltip("The name of this Car Part")]
    public string carPartName;

    [TextArea(5,15)]
    [Tooltip("A description of this Car Part")]
    public string carPartDescription;


    [Header("Pricing Data")]
    [Space(8)]
    [Tooltip("The total cost of this car part to buy")]
    public int cost;

    [Tooltip("The amount of currency you get back when you sell this product")]
    public int sellPrice;

    [Header("Additional Data")]
    [Space(8)]
    [Tooltip("The prefab that belongs to this Car Part")]
    public GameObject prefab;

    [Tooltip("the UI Image used to display this Car Part")]
    public Sprite uiSprite;

}
