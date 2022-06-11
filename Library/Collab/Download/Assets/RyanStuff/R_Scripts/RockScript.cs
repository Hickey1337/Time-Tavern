using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    private Transform thrower; //rock thrower's gameobject
    public float despawnTime = 3f;
    private Rigidbody rb;

    void Start()
    {
        //thrower = GameObject.Find("RockThrowPrototype").transform;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(thrower.transform.forward * 1000);
        //Destroy(gameObject, despawnTime);
    }


    public void Throw()
    {
        //apply force to rock
        GetComponent<Rigidbody>().AddForce(thrower.transform.up * 5000);
    }
}
