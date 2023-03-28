using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "New Order", menuName = "Order")]
public class OrderObject : ScriptableObject
{
    public static OrderObject CurrentOrder;

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
        public string carPartColorName;
    }

    public static bool OrderSelected() => CurrentOrder != null;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Order for {0}:\n", personName);
        sb.AppendFormat("{0}\n", description);

        foreach (OrderCarParts order in orderProducts)
        {
            sb.AppendFormat("- {0}x {1} ({2} {3})\n", order.amount, order.carPart.name, order.carPartColorName, order.carPartColor);
        }

        return sb.ToString();
    }
}
