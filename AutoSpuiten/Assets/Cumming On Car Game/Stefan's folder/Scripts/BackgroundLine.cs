using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundLine : MonoBehaviour
{
    public Image baseImage, dotImage1, dotImage2;

    public Color dot1Offset, baseOffset;

    public Color color1, color2;

    public Color targetColor;

    public float speed;

    float lifeTime;
    public float maxLifeTime;

    Vector3 startPos;
    private void Start()
    {
        startPos = transform.localPosition;
    }
    public void Initialize()
    {
        targetColor = Color.Lerp(color1, color2, Random.Range(1f, 100f) / 100);

        baseImage.color = targetColor - baseOffset;
        dotImage1.color = targetColor - dot1Offset;
        dotImage2.color = targetColor;

        transform.localPosition = startPos;

        speed = 1 + Random.Range(0f, 100f) / 100f;
        lifeTime = maxLifeTime + Random.Range(0,100f) / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Initialize();
        }
    }
}
