using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentOrderItemBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText, typeText, colorText, colorPercentage;

    [SerializeField] OrderObject.OrderCarParts carPart;

    [SerializeField] GameObject completedObject;

    public bool completed;
    public void SetItem(OrderObject.OrderCarParts part)
    {
        carPart = part;
        UpdateOrderItem(0);
    }

    public void UpdateOrderItem(float colorPercentage)
    {
        completedObject.SetActive(completed);

        amountText.text = carPart.amount.ToString();
        typeText.text = carPart.carPart.carPartName;
        colorText.text = carPart.carPartColorName;
        colorText.color = carPart.carPartColor;
        this.colorPercentage.text = colorPercentage.ToString();
    }
}
