using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderProduct : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image orderImage, orderColor;

    public void Initialize(OrderObject.OrderCarParts product)
    {
        text.text = product.amount + "X " + product.carPart.carPartName;

        orderImage.sprite = product.carPart.uiSprite;
        orderColor.color = product.carPartColor;
    }
}
