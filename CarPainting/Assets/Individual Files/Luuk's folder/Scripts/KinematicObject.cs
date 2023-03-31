using UnityEngine;

public class KinematicObject : BasePickUp
{
    public SnapObjects[] targetObjects;
    public float distanceThreshold = 2f;

    private Rigidbody rb;

    Vector3 startPos;
    Quaternion startRot;

    public Vector3 targetSnapEulerAngles;

    CurrentOrderManager com;
    [System.Serializable]
    public struct SnapObjects
    {
        public Transform transform;
        public Vector3 snapEulerAngles;
        public UnityEngine.Events.UnityEvent OnSnapEvent;
        public bool kinematicAfterSnap;
    }
    protected override void Start()
    {
        CurrentOrderManager = FindObjectOfType<CurrentOrderManager>();
        base.Start();
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    public async void CheckSnap()
    {
        await System.Threading.Tasks.Task.Delay(10);

        OnDrop();
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
                if (target.transform.name == "Oven Pivot")
                {
                    var filter = target.transform.GetComponent<MeshFilter>();
                    if()
                    transform.SetPositionAndRotation(target.transform.position, Quaternion.Euler(target.snapEulerAngles));
                    rb.isKinematic = target.kinematicAfterSnap;
                    target.OnSnapEvent.Invoke();
                    return;
                }
            }
        }

        rb.isKinematic = false;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    public override void OnDrop()
    {
        base.OnDrop();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;    
        }
    }
}
 