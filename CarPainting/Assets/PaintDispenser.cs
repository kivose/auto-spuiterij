using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintDispenser : MonoBehaviour
{
    public Image outcomeImage;
    public Color outcome;
    public Vector3Int m_outcome;
    public TextMeshProUGUI outcomeText;

    public Slider slider1;
    public TextMeshProUGUI slider1ValueText;
    public Slider slider2;
    public TextMeshProUGUI slider2ValueText;
    public Slider slider3;
    public TextMeshProUGUI slider3ValueText;

    public bool needsUpdate = true;

    int maxPresets = 16;
    int presetsMade = 0;
    public Transform presets;
    public Dictionary<Vector3Int, PaintDispenserPreset> currentColorPresets;

    public Transform machineObject;

    public Animator animator;

    public Transform player;
    public Outline outline;
    public float distanceTreshold;
    // Update is called once per frame
    void Update()
    {
        if (needsUpdate)
        {
            SetColor(slider1.value, slider2.value, slider3.value);
            needsUpdate = false;
        }
        Enable(Vector3.Distance(player.position,transform.position) <= distanceTreshold);
        

    }

    public void Enable(bool enable)
    {
        animator.SetBool("active", enable);
    }

    public void TryCreatePreset()
    {
        if (!currentColorPresets.ContainsKey(m_outcome))
        {
            presets.GetChild(presetsMade).GetChild(0).GetComponent<PaintDispenserPreset>().Initialize(m_outcome,outcome);
            presetsMade++;
        }
    }
    public void SetDirty()
    {
        needsUpdate = true;
    }

    public void SetColor(int r, int g, int b, int a)
    {
        outcome = new Color32((byte)r, (byte)g, (byte)b, (byte)a);
        m_outcome = new(r, g, b);
        outcomeText.text = m_outcome.ToString();

        outcomeImage.color = outcome;

        slider1ValueText.text = r.ToString();
        slider2ValueText.text = g.ToString();
        slider3ValueText.text = b.ToString();

        outline.OutlineColor = outcome;
    }

    public void SetColor(Vector3Int rgb)
    {
        SetColor(rgb.x, rgb.y, rgb.z);
    }

    public void SetColor(int r, int g, int b)
    {
        SetColor(r, g, b, 255);
    }
    public void SetColor(float r, float g, float b)
    {
        SetColor((int)r,(int)g,(int)b, 255);
    }

    public void SelectPreset(PaintDispenserPreset preset)
    {
        SetColor(preset.m_color);
        UpdateSlider(slider1, preset.m_color.x);
        UpdateSlider(slider2, preset.m_color.y);
        UpdateSlider(slider3, preset.m_color.z);
    }

    public void UpdateSlider(Slider slider, float value)
    {
        slider.value = value;
    }
}
