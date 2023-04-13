using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.UI;
using TMPro;
public class Options : MonoBehaviour
{
    public ActionBasedControllerManager leftHand, rightHand;

    [SerializeField]
    Transform offsetTransform;

    public float camOffset;

    [SerializeField]
    Slider heightSlider, audioSlider;

    [SerializeField]
    TextMeshProUGUI heightValue, audioValue;

    [SerializeField]
    Toggle moveOrTeleportMovement, smoothOrSnapTurning;

    private void Start()
    {
        GetValues();
    }
    public void ApplyValues()
    {
        print("ohneee 1");
        AudioListener.volume = audioSlider.value;
        audioValue.text = audioSlider.value.ToString("F1");

        leftHand.smoothMotionEnabled = !moveOrTeleportMovement.isOn;

        rightHand.smoothTurnEnabled = !smoothOrSnapTurning.isOn;

        camOffset = heightSlider.value;
        heightValue.text = heightSlider.value.ToString("F1");

        offsetTransform.localPosition = new (offsetTransform.localPosition.x, camOffset, offsetTransform.localPosition.z);
    }
    public void ResetValues()
    {
        print("ohneee 2");
        audioSlider.SetValueWithoutNotify(0.5f);
        moveOrTeleportMovement.isOn = false;
        leftHand.smoothMotionEnabled = true;

        smoothOrSnapTurning.isOn = false;
        rightHand.smoothTurnEnabled = true;

        heightSlider.SetValueWithoutNotify(0);
        camOffset = 0;

        ApplyValues();
    }
    public void GetValues()
    {
        print("ohneee 3");
        audioSlider.SetValueWithoutNotify(AudioListener.volume);
        moveOrTeleportMovement.isOn = !leftHand.smoothMotionEnabled;    

        smoothOrSnapTurning.isOn = !rightHand.smoothTurnEnabled;

        camOffset = offsetTransform.localPosition.y;

        heightSlider.SetValueWithoutNotify(camOffset);

        ApplyValues();
    }
}
