using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Yeller : MonoBehaviour
{
    //unique to yeller
    private GameObject nearestAlly; //save position of nearest ally
    private GameObject[] helperList; //saves all the gameobjects of the yeller's allies

    //objects for patrolling behavior
    public float patrolTime = 2f;
    public Transform[] waypoints;

    //how fast to rotate
    public float rotateSpeed = 1000.0f;

    //objects to change material
    Renderer rend;
    public Material[] material;

    //detecting allies collider
    //CapsuleCollider capColl;
    public float detectAlly = 60.0f;
    private float yellingDistance = 20.0f;
    private Transform[] allyList; //yeller gets list of all his allies in the scene


    //private Transform player; //player position
    private int waypointIndex; //index in waypoint list
    private float agentSpeed; //speed of clubholder

    //private Animator anim;
    private NavMeshAgent agent;

    public bool chasing = false; //keeps track if it is chasing
    //private float chaseRange = 10f; //distance after which it will give up chasing

    //yelling distance
    //private capColl

    void Start()
    {
        //get renderer
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0]; //set club man to green

        UpdateListOfAllies();
        UpdateNearestAlly();
    }

    //update yeller's list of allies who are not chasing
    private void UpdateListOfAllies()
    {
        helperList = GameObject.FindGameObjectsWithTag("NotChasing");
    }

    //tell the yeller who the new nearest ally is
    private void UpdateNearestAlly()
    {
        nearestAlly = GetClosestAllyObject(helperList); 
    }

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agentSpeed = agent.speed;
        }

        //randomize first waypoint
        waypointIndex = Random.Range(0, waypoints.Length);

        //repeat functions
        //InvokeRepeating("Tick", 0, 0.5f);

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

    /*
    void Tick()
    {
        
    }
    */

    public void Chase()
    {
        //capColl.setActive();

        rend.sharedMaterial = material[1]; // set yeller man to red
        chasing = true;
        Debug.Log("IS CHASING ALLY");
    }

    public void Normal()
    {
        chasing = false;
        rend.sharedMaterial = material[0]; // set club man to green
        UpdateNearestAlly();
        Debug.Log("NOT CHASING ALLY");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateListOfAllies();
        if (!chasing)
        {
            //set destination of agent based on waypointindex
            agent.destination = waypoints[waypointIndex].position;
            agent.speed = agentSpeed / 2; //lower speed while patrolling
        }
        else
        {
            if (nearestAlly != null)
            {
                float distanceToAlly = Vector3.Distance(transform.position, nearestAlly.transform.position);
                //make NPC's destination the player and make it faster
                if (distanceToAlly < detectAlly)
                {
                    agent.destination = nearestAlly.transform.position;
                    agent.speed = agentSpeed * 2;
                    if (distanceToAlly < yellingDistance)
                    {
                        nearestAlly.GetComponent<ClubHolder>().YelledAt();
                        UpdateListOfAllies();
                        UpdateNearestAlly();
                    }
                }
                else
                {
                    //no ally close enough - start freakin out
                    //Debug.Log("NOT CLOSE ENOUGH - DistanceToAlly: " + distanceToAlly + ", detectAlly: " + detectAlly + ", nearestAlly Position: " + nearestAlly.transform.position);
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                    agent.speed = 0;
                }
            }
            else
            {
                // no allies nearby - start freakin out
                Debug.Log("ALLY NON EXISTENT");
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                agent.speed = 0;
            }
            



            //TO DO: *change this so instead of chasing player yeller runs to ally
            //*Get collider to work with other cavemen
            //*Add another collider for yelling to other cavemen
            //if (nearestAlly != null && debug1 > detectAlly)
            // {
            // Normal();
            //}
            // Debug.Log("Distance to nearestally: " + debug1);
            //Debug.Log("Position of nearestally: " + nearestAlly.position);
            //Debug.Log("Radius of detection: " + detectAlly);
        }

        //unused raycasting code

        //RaycastHit hit;

        ////forward direction vector
        //Vector3 fwd = transform.TransformDirection(Vector3.forward);


        //if (Physics.Raycast(transform.position, fwd, out hit)) //if ray collides with something
        //{
        //    if (hit.collider.tag == "Player") //if ray hits player
        //    {
        //        rend.sharedMaterial = material[1]; // set club man to red

        //        //make NPC's destination the player and make it faster
        //        agent.destination = player.position;
        //        agent.speed = agentSpeed;
        //        Debug.Log("Hit");
        //    }
        //}
        //else //ray not colliding with anything
        //{
        //    rend.sharedMaterial = material[0]; // set club man to green
        //    Debug.Log("No Hit");
        //}
        //Debug.DrawRay(transform.position, fwd * hit.distance, Color.red); //show ray, **not working

    }//end of update

    //taken from https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
    Transform GetClosestAlly(Transform[] allies)
    {
        Transform closestAlly = null; //transform to return
        float smallestDistance = 1000.0f; //big number that enemy should be lower than
        Vector3 currentPosition = transform.position; //position of yeller

        foreach (Transform t in allies)
        {
            float distance = Vector3.Distance(t.position, currentPosition);
            if (distance < smallestDistance) //if distance to ally is less than smallest
            {
                closestAlly = t; //set new closest ally
                smallestDistance = distance; //update smallest distance
            }
        }

        return closestAlly; //return the transform
    }

    GameObject GetClosestAllyObject(GameObject[] allies)
    {
        GameObject closestAlly = null; //transform to return
        float smallestDistance = 1000.0f; //big number that enemy should be lower than
        Vector3 currentPosition = transform.position; //position of yeller

        foreach (GameObject g in allies)
        {
            float distance = Vector3.Distance(g.transform.position, currentPosition);
            //Debug.Log(distance + " and " + smallestDistance);
            if (distance < smallestDistance) //if distance to ally is less than smallest
            {
                //Debug.Log(g.GetComponent<ClubHolder>().chasing);
                //if (g.GetComponent<ClubHolder>().chasing)
                //{
                    closestAlly = g; //set new closest ally
                    smallestDistance = distance; //update smallest distance
                //}
            }
        }

        return closestAlly; //return the object
    }

    //helper function to turn gameobject array into transform array of those gameobjects
    Transform[] GetTransformsFromList(GameObject[] list)
    {
        Transform[] result = new Transform[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            result[i] = list[i].transform;
        }

        return result;
    }
}

