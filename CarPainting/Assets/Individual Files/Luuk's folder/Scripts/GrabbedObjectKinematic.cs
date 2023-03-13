using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbedObjectKinematic : MonoBehaviour
{
    public GameObject rigidy;
    public Rigidbody ridigy;

    public void OnSelectExit(XRBaseInteractor interactor)
    {
        ridigy.constraints = RigidbodyConstraints.None;
    }

}
