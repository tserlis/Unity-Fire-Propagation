using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{

    public GameObject fireAttack;

    private int layerMask = 1 << 8;
    

    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1")) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 25f, layerMask)) {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                Debug.Log("Hit");
                Instantiate(fireAttack, hit.collider.gameObject.transform);
                
            }
            else {
                Debug.DrawRay(transform.position, transform.forward * 25f, Color.red);
                Debug.Log("No Hit");
            }
        }
    }
}
