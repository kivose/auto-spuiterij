using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentOrderItemBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText, typeText, colorText;

    [SerializeField] OrderObject.OrderCarParts carPart;

    [SerializeField] GameObject completedObject;

    public bool completed;
    public void SetItem(OrderObject.OrderCarParts part)
    {
        carPart = part;
        UpdateOrderItem();
    }

    public void UpdateOrderItem()
    {
        completedObject.SetActive(completed);

        amountText.text = carPart.amount.ToString();
        typeText.text = carPart.carPart.carPartName;
        colorText.text = carPart.carPartColorName;
        colorText.color = carPart.carPartColor;
    }
}
