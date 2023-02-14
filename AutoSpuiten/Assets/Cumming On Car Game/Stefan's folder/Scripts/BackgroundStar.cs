using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundStar : MonoBehaviour
{
    public Image pointOfLight, hexagon;

    public Color hexagonOffset;

    public Color color1, color2;

    public Color targetColor;

    public Transform upperRight, lowerLeft;

    public Animator animator;

    public float time = 0.5f;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        time = 0.5f + Random.Range(-100f,100f) / 100f;

        animator.SetTrigger("Active");

        targetColor = Color.Lerp(color1, color2, Random.Range(1f, 100f) / 100);

        pointOfLight.color = targetColor;
        hexagon.color = targetColor - hexagonOffset;

        transform.localPosition = RandomPosOnScreen();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Initialize();
        }
    }
    public Vector3 RandomPosOnScreen()
    {
        return new Vector3(Random.Range(upperRight.transform.localPosition.x, lowerLeft.transform.localPosition.x), Random.Range(upperRight.transform.localPosition.y, lowerLeft.transform.localPosition.y));
    }
}
