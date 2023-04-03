using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdditionalSlider : MonoBehaviour
{
    public ColorChanger bgParent;

    public ColorChanger fillParent;
    // Start is called before the first frame update
    void Start()
    {
        Image[] imgs = new Image[bgParent.images.Length];
        for (int i = 0; i < bgParent.images.Length; i++)
        {
            imgs[i] = Instantiate(bgParent.transform.GetChild(i), fillParent.transform).GetComponent<Image>();
        }
        fillParent.images = imgs;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bgParent.images.Length; i++)
        {
            fillParent.images[i].transform.position = bgParent.images[i].transform.position;
        }
    }
}
