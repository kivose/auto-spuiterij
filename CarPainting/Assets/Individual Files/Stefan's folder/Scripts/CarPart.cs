using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPart : BasePickUp
{
    public override void OnPickUp()
    {
        base.OnPickUp();

        GetComponent<MeshCollider>().enabled = false;
    }
    public override void OnDrop()
    {
        base.OnPickUp();

        GetComponent<MeshCollider>().enabled = true;
    }
}
