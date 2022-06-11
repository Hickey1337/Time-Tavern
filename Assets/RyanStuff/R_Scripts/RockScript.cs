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
        rb = gameObject.GetComponent<Rigidbody>();
        
        GameObject target = GameObject.Find("VrBody");
        //gameObject.transform.LookAt(target.transform);
        rb.AddForce((target.transform.position - transform.position) * 10);
        Debug.Log(target.name);
        Destroy(gameObject, despawnTime);
    }


    public void Throw()
    {
        //apply force to rock
        GetComponent<Rigidbody>().AddForce(thrower.transform.up * 5000);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide " +collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger " +other.gameObject.name);
    }
}
