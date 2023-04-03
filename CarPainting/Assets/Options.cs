using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    public ActionBasedControllerManager leftHand, rightHand;

    [SerializeField]
    Transform offsetTransform;

    public float camOffset;

    [SerializeField]
    Slider heightSlider;

    [SerializeField]
    Toggle moveOrTeleportMovement, smoothOrSnapTurning;

    private void Start()
    {
        GetValues();
    }
    public void ApplyValues()
    {
        leftHand.smoothMotionEnabled = !moveOrTeleportMovement.isOn;

        rightHand.smoothTurnEnabled = !smoothOrSnapTurning.isOn;

        camOffset = heightSlider.value;

        offsetTransform.localPosition = new (offsetTransform.localPosition.x, camOffset, offsetTransform.localPosition.z);
    }
    public void ResetValues()
    {
        moveOrTeleportMovement.isOn = false;
        leftHand.smoothMotionEnabled = true;

        smoothOrSnapTurning.isOn = false;
        rightHand.smoothTurnEnabled = true;

        heightSlider.value = 0;
        camOffset = 0;
    }
    public void GetValues()
    {
        moveOrTeleportMovement.isOn = !leftHand.smoothMotionEnabled;

        smoothOrSnapTurning.isOn = !rightHand.smoothTurnEnabled;

        camOffset = offsetTransform.localPosition.y;

        heightSlider.value = camOffset;
    }
}
