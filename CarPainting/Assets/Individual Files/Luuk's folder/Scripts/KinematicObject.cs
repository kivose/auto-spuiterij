using UnityEngine;

public class KinematicObject : BasePickUp
{
    public GameObject targetObject;
    public float distanceThreshold = 2f;

    private Rigidbody rb;

    Vector3 startPos;
    Quaternion startRot;

    public Vector3 targetSnapEulerAngles;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void CheckSnap()
    {
        // Check if object can snap to base position

        if (Vector3.Distance(transform.position, startPos) < distanceThreshold)
        {
            transform.SetPositionAndRotation(startPos, startRot);

            rb.isKinematic = true;
        }
        else if (targetObject != null && Vector3.Distance(transform.position, targetObject.transform.position) < distanceThreshold)
        {
            transform.SetPositionAndRotation(targetObject.transform.position,Quaternion.Euler(targetSnapEulerAngles));
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        GetComponent<Collider>().enabled = false;
    }
    public override void OnDrop()
    {
        base.OnDrop();
        GetComponent<Collider>().enabled = true;
    }
}
 