using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public bool holdPosition; //should an enemy hold at this waypoint
    public Transform _wayTransform; //the transform of this waypoint
    public Vector3 wayPosition; //position of this waypoint
    public float holdTime; //amount of time to spend at this waypoint if holding

    // Start is called before the first frame update
    void Start()
    {
        holdPosition = false;
        _wayTransform = transform;
        wayPosition = _wayTransform.position;
        holdTime = 4.0f;
    }

    //set the value of the holdposition boolean
    public void setHold(bool input)
    {
        holdPosition = input;
    }

}
