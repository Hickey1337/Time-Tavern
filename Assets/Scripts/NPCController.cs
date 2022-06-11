using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints;

    private int waypointIndex;
    private float speed, agentSpeed;
    private Transform player;

    //private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agentSpeed = agent.speed;
        }

        //get player x,y,z component
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //randomize first waypoint
        waypointIndex = Random.Range(0, waypoints.Length);

        //repeat functions
        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }
    }

    void Patrol()
    {
        //changing waypointIndex based on WHAT IS LIKE AN IF ELSE STATEMENT; setting it equal to 0 if waypointIndex is the LAST waypoint, else set it to waypointIndex + 1 (the next waypoint)
        //what I think this does is cycle through each waypoint, going back to the beginning if the last waypoint is reached
        waypointIndex = waypointIndex == waypoints.Length - 1 ? 0 : waypointIndex + 1;
    }

    void Tick()
    {
        //set destination of agent based on waypointindex
        agent.destination = waypoints[waypointIndex].position;
        agent.speed = agentSpeed / 2; //lower speed while patrolling

        //if player is within aggro range
        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            //make NPC's destinationt the player
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }
}
