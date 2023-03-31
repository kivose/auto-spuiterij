using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Transform invalidPivot;

    private void OnCollisionEnter(Collision collision)
    {
        var kin = collision.transform.GetComponent<KinematicObject>();
        if(kin != null && kin.isSuccesfullyPainted)
        {
            kin.complete = true;
        }
    }
}
