using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CurrentOrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderName, Description, noOrders;

    public GameObject orderItemsPrefab;
    public Transform orderItemsParent;
    List<CurrentOrderItemBehaviour> activeCurrentOrders = new();


    private void OnEnable()
    {
        InitializeOrderItems();
    }
    public void InitializeOrderItems()
    {
        for (int i = 0; i < activeCurrentOrders.Count; i++)
        {
            Destroy(activeCurrentOrders[i].gameObject);
        }

        activeCurrentOrders.Clear();

        var currentOrder = OrderObject.CurrentOrder;

        if (currentOrder == null)
        {
            orderName.text = "";
            Description.text = "";
            noOrders.text = " n o    o r d e r s   s e l e c t e d";
        }
        else
        {
            orderName.text = currentOrder.personName;
            Description.text = currentOrder.description;
            noOrders.text = "";

            for (int i = 0; i < currentOrder.orderProducts.Length; i++)
            {
                var item = Instantiate(orderItemsPrefab, orderItemsParent).GetComponent<CurrentOrderItemBehaviour>();

                item.SetItem(currentOrder.orderProducts[i]);

                activeCurrentOrders.Add(item);
            }
        }
    }
    void UpdateOrderItems()
    {
        for (int i = 0; i < activeCurrentOrders.Count; i++)
        {
            activeCurrentOrders[i].UpdateOrderItem();
        }
    }
}
