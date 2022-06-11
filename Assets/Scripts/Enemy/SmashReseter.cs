using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tell chieftain when player has entered or left his land
public class SmashReseter : MonoBehaviour
{
    //when player enters chief's land, reset the smash time
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<R_Chieftain>().resetSmashTime();
            //Debug.Log("COLLIDER WORKED");
        }
    }

    //when player leaves chieftain's land, check if he can calm down
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<R_Chieftain>().TryToCalmDown();
        }
    }
}
