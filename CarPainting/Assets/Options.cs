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
    
    public void ApplyValues()
    {

    }
    public void ResetValues()
    {

    }
    public void GetValues()
    {

    }
}
