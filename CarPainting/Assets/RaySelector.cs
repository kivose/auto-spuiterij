using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaySelector : MonoBehaviour
{
    public XRRayInteractor ray, rayGrab;

    void Update()
    {
        if(Physics.Raycast(transform.position,transform.forward, out RaycastHit hit))
        {
            var interactable = hit.collider.GetComponent<XRBaseInteractable>();

            if (interactable)
            {
                if (HasInteractionLayerOverlap(rayGrab, interactable))
                {
                    rayGrab.gameObject.SetActive(true);
                    ray.gameObject.SetActive(false);
                }
                if (HasInteractionLayerOverlap(ray, interactable))
                {
                    ray.gameObject.SetActive(true);
                    rayGrab.gameObject.SetActive(false);
                }
            }
            else
            {
                rayGrab.gameObject.SetActive(true);
                ray.gameObject.SetActive(true);
            }
        }
    }

    public bool HasInteractionLayerOverlap(IXRInteractor interactor, IXRInteractable interactable)
    {
        return (interactor.interactionLayers & interactable.interactionLayers) != 0;
    }
}
