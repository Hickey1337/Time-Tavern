using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the purpose of this script is to assign the point to a vector 3 zero
//this vector can be changed to act as an offset for the distance the object is to be held in the hand from if needed
public class KatanaSetup : MonoBehaviour
{
    public bool held = false;
    public GameObject light;

    void Update()
    {
        if(gameObject.GetComponent<ConfigurableJoint>() == null && held)
        {
            held = false;
            tagChildren(0);
            Debug.Log("dropped");
            if(light != null)
                light.SetActive(false);
        }
        else if(gameObject.GetComponent<ConfigurableJoint>() != null && !held)
        {
            held = true;
            gameObject.GetComponent<ConfigurableJoint>().connectedAnchor = new Vector3(0, 0, 0);
            tagChildren(10);
            Debug.Log("grabbed");
            if (light != null)
                light.SetActive(true);
        }

    }

    //puts all children onto the same layer
    private void tagChildren(int newLay)
    {
        gameObject.layer = newLay;
        foreach(Transform child in transform)
        {
            if(child.gameObject.layer != newLay)
            {
                child.gameObject.layer = newLay;
            }
        }
    }
}
