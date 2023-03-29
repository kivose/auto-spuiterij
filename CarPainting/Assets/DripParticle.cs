using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripParticle : MonoBehaviour
{
    public Mesh mesh;
    public ParticleSystem particleSystem;
    public Vector3 eulerAngles;

    public Color AverageColor
    {
        get
        {
            float newR = 0;
            float newG = 0;
            float newB = 0;

            float total = 0;
            foreach (Color color in mesh.colors)
            {
                newR += color.r;
                newG += color.g;
                newB += color.b;

                total++;
            }

            return new Color
            {
                r = newR / total,
                g = newG / total,
                b = newB / total
            };
        }
    }

    bool initialized = false;
    public void Initialize(Mesh mesh)
    {
        if (initialized) return;

        particleSystem = GetComponent<ParticleSystem>();
        Debug.Log("initialize started");
        this.mesh = mesh;
        initialized = true;

        particleSystem.Play();
    }

    private void FixedUpdate()
    {
        if (!initialized) return;

        var main = particleSystem.main;
        var color = AverageColor;
        color += new Color(0, 0, 0, 255);
        main.startColor = new ParticleSystem.MinMaxGradient(color);
    }

}
