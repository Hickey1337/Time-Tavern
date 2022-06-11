using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeCHScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //tell the parent
            //clubholder refers to clubholder script
            if (transform.parent.gameObject.GetComponent<ClubHolder>() != null)
            {
                transform.parent.gameObject.GetComponent<ClubHolder>().Chase();
            }
            else if (transform.parent.gameObject.GetComponent<RockThrower>() != null)
            {
                transform.parent.gameObject.GetComponent<RockThrower>().Chase();
            }
            else
                Debug.LogError("Not a Club Holder or Rock Thrower");
            //transform.parent.gameObject.tag = "Chasing"
        }
    }
}
