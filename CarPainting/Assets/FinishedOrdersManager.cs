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
    public TextMeshProUGUI personName, description, orderAmountText;


    public GameObject orderPrefab;

    public Transform carPartOrdersParent;

    List<GameObject> activeCarParts = new();

    private void Start()
    {
        SelectedOrderIndex = 0;
    }

    void OnSelectedOrderChanged()
    {
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
        if (add)
            SelectedOrderIndex++;
        else
            SelectedOrderIndex--;
    }
}

