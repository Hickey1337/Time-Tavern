using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    private GameObject _parent;

    void Awake()
    {
         _parent = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //tell the parent (an enemy type) to chase
            _parent.GetComponent<Enemy>().Chase();
        }
    }

}
