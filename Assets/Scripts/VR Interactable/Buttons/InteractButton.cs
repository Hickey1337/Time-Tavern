using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables.ArtificialBased;

[RequireComponent(typeof(VRTK_ArtificialPusher))]
public abstract class InteractButton : MonoBehaviour
{
    private VRTK_ArtificialPusher _pusher;

    private void Awake()
    {
        _pusher = gameObject.GetComponent<VRTK_ArtificialPusher>();
    }

    //checks to see if the button is fully pressed
    public bool getPressed()
    {
        if (_pusher.GetNormalizedValue() == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}