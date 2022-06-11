using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_VisionCone : MonoBehaviour
{
    private GameObject _parent;

    void Awake()
    {
         _parent = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with " + other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            //tell the parent (an enemy type) to chase
            _parent.GetComponent<R_Enemy>().Chase();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with " + collision.gameObject.tag);
    }
}
