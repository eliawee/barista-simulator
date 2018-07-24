using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrab : MonoBehaviour
{
    [SerializeField]
    private Transform interactables;

    [SerializeField]
    private Transform hand;

    private List<Transform> colliding = new List<Transform>();
    private Transform grabbed;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Grabbable")
        {
            colliding.Add(col.transform);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Grabbable")
        {
            colliding.Remove(col.transform);
        }
    }

    public void BeginGrab()
    {
        if (colliding.Count > 0)
        {
            grabbed = colliding[0];
            grabbed.GetComponent<Rigidbody>().isKinematic = true;
            grabbed.parent = hand;
        }
    }

    public void EndGrab()
    {
        if (grabbed != null)
        {
            grabbed.parent = interactables;
            grabbed.GetComponent<Rigidbody>().isKinematic = false;
            grabbed = null;
        }
    }
}
