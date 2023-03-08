using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbedObjectKinematic : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void DisableKinematic()
    {
        rb.isKinematic = false;
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        Invoke("DisableKinematic", 0.1f);
    }
}
