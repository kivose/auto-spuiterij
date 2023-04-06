using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenOrdersManager : MonoBehaviour
{
    [SerializeField]
    public List<OrderObject> allOrders;

    public OrderObject currentOrder;
    private int m_currentOrderIndex;
    public int CurrentOrderIndex
    {
        get
        {
            return m_currentOrderIndex;
        }
        set
        {
            if (value < 0) value = allOrders.Count -1;
            else if (value >= allOrders.Count) value = 0;

            m_currentOrderIndex = value;
            currentOrder = allOrders[m_currentOrderIndex];

            OnCurrentOrderChanged();
        
        }
    }

    [Header("References")]
    public TextMeshProUGUI personName, description, orderAmountText;


    public GameObject orderPrefab;

    public Transform carPartOrdersParent;

    List<GameObject> activeCarParts = new();

    public GameObject selectOrderButton;

    public GameObject selectedFilter;

    public CurrentOrderManager currentOrderManager;
    private void OnEnable()
    {
        CurrentOrderIndex = 0;
        OnCurrentOrderChanged();
        Update();
    }

    public void ApplyCurrentOrder()
    {
        print("current Order Applied");
        OrderObject.CurrentOrder = currentOrder;
        currentOrderManager.InitializeOrderItems();
        currentOrderManager.OnOrderChanged(CurrentOrderIndex);
    }

    public void CancelCurrentOrder()
    {
        OrderObject.CurrentOrder = null;
    }

    private void Update()
    {
        selectOrderButton.SetActive(!OrderObject.OrderSelected());
        selectedFilter.SetActive(OrderObject.OrderSelected());
    }

    void OnCurrentOrderChanged()
    {
        orderAmountText.text = CurrentOrderIndex + 1 + " / " + allOrders.Count;
        personName.text = currentOrder.personName;
        description.text = currentOrder.description;

        for (int i = 0; i < activeCarParts.Count; i++)
        {
            Destroy(activeCarParts[i]);
        }

        activeCarParts.Clear();

        for (int i = 0; i < currentOrder.orderProducts.Length; i++)
        {
            var product = currentOrder.orderProducts[i];

            var order = Instantiate(orderPrefab, carPartOrdersParent).GetComponent<OrderProduct>();

            order.Initialize(currentOrder.orderProducts[i]);

            activeCarParts.Add(order.gameObject);
        }
    }

    public void ScrollThroughOrders(bool add)
    {
        if (add)
            CurrentOrderIndex++;
        else
            CurrentOrderIndex--;
    }
}
