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

    public Transform mainCar;

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

    private void FixedUpdate()
    {
        CheckOrderCompletion();
    }

    public class CarObjectData
    {
        public Transform transform;
        public CarPartObject carPartObject;
        public MeshFilter filter;
    }

    public List<CarObjectData> carObjects = new List<CarObjectData>();
    void CheckOrderCompletion()
    {
        for (int i = 0; i < carObjects.Count; i++)
        {
            float colorPercentage = CalculateColorPercentage(carObjects[i].filter.sharedMesh.colors, OrderObject.CurrentOrder.orderProducts[i].carPartColor);
            if(activeCurrentOrders[i].carObject == null)
            {
                activeCurrentOrders[i].carObject = carObjects[i].transform;
                activeCurrentOrders[i].mesh = carObjects[i].filter.sharedMesh;
            }
            activeCurrentOrders[i].UpdateOrderItem(colorPercentage);
        }
    }

    public void OnOrderChanged()
    {
        carObjects.Clear();

        carObjects = FindCarObjects();
    }

    public List<CarObjectData> FindCarObjects()
    {
        List<CarObjectData> objects = new();

        for (int i = 0; i < OrderObject.CurrentOrder.orderProducts.Length; i++)
        {
            var currentProduct = OrderObject.CurrentOrder.orderProducts[i];
            GameObject carObject = currentProduct.carPart.CarPartGameObject;

            if (carObject)
            {
                objects.Add(new CarObjectData
                {
                    transform = carObject.transform,
                    filter = carObject.transform.GetComponent<MeshFilter>(),
                    carPartObject = currentProduct.carPart
                });
            }
            else
            {
                throw new System.Exception($"Car Object hasnt been found! Index : {i}, Current Product :" + OrderObject.CurrentOrder.ToString());
            }
        }

        return objects;
    }

    public int colorCalculationIterations;
    public float CalculateColorPercentage(Color[] vertexColors, Color targetColor)
    {
        float total = 0;
        int totalIterations = 0;
        for (int i = 0; i < vertexColors.Length; i+= colorCalculationIterations)
        {
            total += CompareColors(vertexColors[i], targetColor);
            totalIterations = i;
        }
        return Mathf.Min(100,Mathf.Abs((total / totalIterations -1) * 1.05f));
    }

    public float CompareColors(Color color1, Color color2)
    {
        float redDiff = Mathf.Abs(color1.r - color2.r);
        float greenDiff = Mathf.Abs(color1.g - color2.g);
        float blueDiff = Mathf.Abs(color1.b - color2.b);

        float maxDiff = Mathf.Max(redDiff, greenDiff, blueDiff);
        float percentDiff = maxDiff / 1.0f * 100.0f;

        return 100.0f - percentDiff;
    }
}
