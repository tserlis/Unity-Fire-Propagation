using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform pickupPoint;

    void OnMouseDown() 
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.position = pickupPoint.position;
        this.transform.parent = GameObject.Find("Pickup Point").transform;
    }

    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;

    }
}
