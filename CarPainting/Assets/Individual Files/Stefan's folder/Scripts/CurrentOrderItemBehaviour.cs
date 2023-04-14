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

    public Transform carObject { get; set; }
    public Mesh mesh { get; set; }

    Color startColor;
    private void Start()
    {
        startColor = new Color(0.5f,1f,1f,1f);
    }
    public void SetItem(OrderObject.OrderCarParts part)
    {
        carPart = part;
    }

    public float paintDripTreshold = 60;
    public void UpdateOrderItem(float colorPercentage)
    {
        if (carObject == null) completed = true;

        completedObject.SetActive(completed);

        amountText.text = carPart.amount.ToString();
        typeText.text = carPart.carPart.carPartName;
        colorText.text = carPart.carPartColorName;
        colorText.color = carPart.carPartColor;
        this.colorPercentage.text = colorPercentage.ToString("F");

        if(colorPercentage > paintDripTreshold)
        {
            print("Drip start");
            var particle = carObject.GetComponentInChildren<DripParticle>();
            if (particle)
            {
                particle.Initialize(mesh);
            }
        }


        this.colorPercentage.color = colorPercentage >= 100? Color.green : startColor;
        ToggleCompletedObject(completed);
    }

    public void HighlightCarPart()
    {
        if (carObject)
        {
            Outline outline = carObject.GetComponent<Outline>();

            if (outline)
            {
                outline.Highlight();
                Debug.Log("Highlighting");
            }
            else
                Debug.LogWarning("Outline not found!", gameObject);
        }
        else
            Debug.LogWarning("Car Object not found!", gameObject);
    }

    public void ToggleCompletedObject(bool enable) => completedObject.SetActive(enable);
}
