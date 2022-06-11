using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chieftain : Enemy
{
    //radius of Chieftain's land
    //could become obsolete if collider becomes more than just a circle
    public float detectPlayer = 30.0f;

    public float chieftainPatience = 4; //time player can stand in chieftain land before he chases
    private float timeToSmash = 0; //time after which chieftain will attack

    //reset smash time of chieftain
    //aka reset chieftain's patience when player enters land again
    public void resetSmashTime()
    {
        timeToSmash = Time.time + chieftainPatience;
    }

    //giving warning to player
    //this way he won't insta aggro on player entering land
    public void GivingWarning()
    {
        agentSpeed = 0;
        _agent.speed = agentSpeed / 2;
        _agent.destination = base.player.position;
    }

    //called when player exits collider
    public bool TryToCalmDown()
    {
        bool canCalmDown = false;

        if (Time.time < timeToSmash)
        {
            canCalmDown = true;
            agentSpeed = 5;
            _agent.speed = agentSpeed / 2;
            state = State.NOTCHASING;
        }

        return canCalmDown;
    }

    public override void Update()
    {

        //sees player and is giving warning
        if ( (base.player != null) && (Vector3.Distance(transform.position, base.player.position)) < detectPlayer && (Time.time < timeToSmash) && (state == State.NOTCHASING) )
        {

            //Debug.Log("IN MA SWAMP");
            GivingWarning();
            state = State.CHASING;

        }
        //warning is over, chase player
        //simply add back the distance checker to make the chieftain deaggro
        else if ( (base.player != null)  && (Time.time >= timeToSmash) && (state == State.CHASING) )
        {

            agentSpeed = 5;
            _agent.speed = agentSpeed;
            _agent.destination = base.player.position;
            base.Chase();

        }
        //if chieftain has been yelled at by yeller
        else if ( state == State.YELLEDAT )
        {
            agentSpeed = 5;
            _agent.speed = agentSpeed;
            _agent.destination = base.player.position;
            base.Chase();
        }
        //go back to normal
        else if ( state == State.NOTCHASING )
        {

            //set destination of agent based on waypointindex
            _agent.destination = waypoints[waypointIndex].position;
            _agent.speed = agentSpeed / 2; //lower speed while patrolling
            base.Normal();
        }
    }

}
