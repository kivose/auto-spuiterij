using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Order")]
public class OrderObject : ScriptableObject
{
    public string personName;

    [TextArea(5,15)]
    public string description;

    public OrderCarParts[] orderProducts;

    [System.Serializable]
    public struct OrderCarParts
    {
        public int amount;
        public CarPartObject carPart;
        public Color carPartColor;
    }
}
