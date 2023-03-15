using UnityEngine;

public class KinematicObject : MonoBehaviour
{
    public GameObject targetObject;
    public float distanceThreshold = 2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Check the distance between this object and the target object
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // If the distance is less than the threshold, set the object's isKinematic property to true
        if (distance < distanceThreshold)
        {
            rb.isKinematic = true;
        }
        // Otherwise, set the object's isKinematic property to false
        else
        {
            rb.isKinematic = false;
        }
    }
}
