using UnityEngine;

public class KinematicObject : BasePickUp
{
    public SnapObjects[] targetObjects;
    public float distanceThreshold = 2f;

    private Rigidbody rb;

    Vector3 startPos;
    Quaternion startRot;

    public Vector3 targetSnapEulerAngles;

    [System.Serializable]
    public struct SnapObjects
    {
        public Transform transform;
        public Vector3 snapEulerAngles;
        public UnityEngine.Events.UnityEvent OnSnapEvent;
        public bool kinematicAfterSnap;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void CheckSnap()
    {
        // ==> Check if object can snap to base position

        if (Vector3.Distance(transform.position, startPos) < distanceThreshold)
        {
            transform.SetPositionAndRotation(startPos, startRot);

            rb.isKinematic = true;
            return;
        }

        // ==> Check if object can snap to snappable objects
        for (int i = 0; i < targetObjects.Length; i++)
        {
            var target = targetObjects[i];
            float dst = Vector3.Distance(target.transform.position, transform.position);

            if(dst <= distanceThreshold)
            {
                transform.SetPositionAndRotation(target.transform.position, Quaternion.Euler(target.snapEulerAngles));
                rb.isKinematic = target.kinematicAfterSnap;
                target.OnSnapEvent?.Invoke();
                return;
            }
        }

        rb.isKinematic = false;
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
 