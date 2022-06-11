using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationEvents : MonoBehaviour
{
    public GameObject lights;
    public GameObject warpSpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flipLights(bool m_act)
    {
        lights.SetActive(m_act);
        //warpSpot.SetActive(!m_act);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            flipLights(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            flipLights(false);
        }
    }
}
