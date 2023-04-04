using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishedOrdersManager : MonoBehaviour
{
    public List<OrderObject> finishedOrders;

    public OrderObject selectedOrder;
    private int m_SelectedOrderIndex;
    public int SelectedOrderIndex
    {
        get
        {
            return m_SelectedOrderIndex;
        }
        set
        {
            if (value != m_SelectedOrderIndex)
            {
                if (value < 0) value = finishedOrders.Count - 1;
                else if (value >= finishedOrders.Count) value = 0;

                value = Mathf.Clamp(value, 0, Mathf.Max(finishedOrders.Count - 1, 0));

                m_SelectedOrderIndex = value;
                selectedOrder = finishedOrders[m_SelectedOrderIndex];

                OnSelectedOrderChanged();
            }
        }
    }

    [Header("References")]
    public TextMeshProUGUI personName, description, orderAmountText,noOrdersText;


    public GameObject orderPrefab;

    public Transform carPartOrdersParent;

    List<GameObject> activeCarParts = new();


    private void OnEnable()
    {
        SelectedOrderIndex = 0;
    }
    void EnableFinishedOrdersText(bool value)
    {
        noOrdersText.gameObject.SetActive(value);
    }
    void OnSelectedOrderChanged()
    {
        if(finishedOrders.Count == 0)
        {
            EnableFinishedOrdersText(true);
            return;
        }

        EnableFinishedOrdersText(false);


        orderAmountText.text = SelectedOrderIndex + 1 + " / " + finishedOrders.Count;
        personName.text = selectedOrder.personName;
        description.text = selectedOrder.description;

        for (int i = 0; i < activeCarParts.Count; i++)
        {
            Destroy(activeCarParts[i]);
        }

        activeCarParts.Clear();

        for (int i = 0; i < selectedOrder.orderProducts.Length; i++)
        {
            var product = selectedOrder.orderProducts[i];

            var order = Instantiate(orderPrefab, carPartOrdersParent).GetComponent<OrderProduct>();

            order.Initialize(selectedOrder.orderProducts[i]);

            activeCarParts.Add(order.gameObject);
        }
    }

    public void ScrollThroughOrders(bool add)
    {
        if (finishedOrders.Count == 0)
        {
            EnableFinishedOrdersText(true);
            return;
        }

        if (add)
            SelectedOrderIndex++;
        else
            SelectedOrderIndex--;
    }
}

