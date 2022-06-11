using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeYeller : MonoBehaviour
{
    /*
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("COLLISION");
        if(col.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<ClubHolder>().Chase();
            //Debug.Log("TRIGGERED");
        }
        Debug.Log(col.gameObject.tag + "Trigger");
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            transform.parent.gameObject.GetComponent<ClubHolder>().Normal();
            Debug.Log("EXIT");
        }
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //tell the parent
            //yeller refers to yeller script
            transform.parent.gameObject.GetComponent<Yeller>().Chase();
        }
    }

    /*
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<ClubHolder>().Normal();
            transform.parent.gameObject.GetComponent<ClubHolder>().chasing = false;
            Debug.Log("EXIT");
        }
    }
    */
}
