using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Color color1, color2;

    public float randomOffset;

    public float baseColorLerpSpeed;

    public Image[] images;

    public ImageData[] imageDatas;
    public struct ImageData
    {
        public Color color;
        public float currentTimer;
        public float maxTimer;
        public Vector3 targetScale;
        public Vector3 baseScale;
    }

    public Vector3 minScale;
    private void Start()
    {
        imageDatas = new ImageData[images.Length];
        for(int i = 0; i < imageDatas.Length; i++)
        {
            imageDatas[i].baseScale = images[i].transform.localScale;
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.Lerp(images[i].color, imageDatas[i].color, Time.fixedDeltaTime / imageDatas[i].currentTimer);
            images[i].transform.localScale = Vector3.Lerp(images[i].transform.localScale, imageDatas[i].targetScale, Time.fixedDeltaTime / imageDatas[i].currentTimer);

            imageDatas[i].currentTimer -= Time.fixedDeltaTime;
            if (imageDatas[i].currentTimer <= 0)
            {
                imageDatas[i].maxTimer = baseColorLerpSpeed + Random.Range(-randomOffset * 100, randomOffset * 100) / 500f;
                imageDatas[i].currentTimer = imageDatas[i].maxTimer;
                imageDatas[i].color = Color.Lerp(color1, color2, Random.Range(1f, 100f) / 100);
                imageDatas[i].targetScale = Vector3.Lerp(imageDatas[i].baseScale, minScale, Random.Range(1f, 100f) / 100);
            }
        }
    }
}
