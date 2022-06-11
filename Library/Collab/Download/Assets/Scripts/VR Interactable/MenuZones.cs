using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZones : MonoBehaviour
{
    public GameObject activator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            activator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activator.SetActive(false);
        }
    }
}
