using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;

public class AudioButton : InteractButton
{
    // Update is called once per frame
    void Update()
    {
        if (getPressed())
        {
            Debug.Log("audio pressed");
            //play audio here

        }

        
    }
}
