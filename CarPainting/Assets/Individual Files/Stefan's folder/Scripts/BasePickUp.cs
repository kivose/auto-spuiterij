using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class BasePickUp : MonoBehaviour
{
    XRGrabInteractable grab;

    public Collider[] colliders;
    [Tooltip("Boolean that determinse if this gameObject is picked up")]
    public bool pickedUp;

    
    protected virtual void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(delegate { OnPickUp(); });
        grab.selectExited.AddListener(delegate { OnDrop(); });
    }

    /// <summary>
    /// Function called whenever the object is picked up
    /// </summary>
    public virtual void OnPickUp()
    {
        pickedUp = true;
    }

    /// <summary>
    /// Function called whenever the object is dropped
    /// </summary>
    public virtual void OnDrop()
    {
        pickedUp = false;
    }

    public void EnableColliders(bool value)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = value;
        }
    }
}
