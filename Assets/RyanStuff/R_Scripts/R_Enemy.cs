using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TO DO: Get rid of tag usage
//       Make other enemies use this
//       Make this as abstract as possible
public class R_Enemy : MonoBehaviour
{

    public enum State {NOTCHASING, CHASING, YELLEDAT, YELLER, INFCHASE};

    public State state;
    //objects for patrolling behavior
    public float patrolTime = 2f;
    public Waypoint[] waypoints;

    private float stopHoldingTime; //variable for the time when the enemy can move again (for when they are holding position)
    //public float holdDistance; //distance away from waypoint enemy should hold; should not be smaller than 3f

    public Transform player; //player position
    protected int waypointIndex; //index in waypoint list

    [SerializeField]
    protected float agentSpeed; //speed of clubholder

    [SerializeField]
    protected Animator _anim;

    protected NavMeshAgent _agent;

    [SerializeField]
    private float chaseRange = 10f; //distance after which it will give up chasing
    [SerializeField]
    protected float attackRange;
    protected float attackTime = 2;
    protected float attackCooldown; //when attack time equals this value the enemy will attack it is randomized by the children scripts

    protected virtual void Start()
    {
        state = State.NOTCHASING;
        stopHoldingTime = 0;
    }

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        //get agent component
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            agentSpeed = _agent.speed; //set default speed
        }

        //get player x,y,z component
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //randomize first waypoint
        waypointIndex = 0;


        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime); //makes enemy patrol every patrolTime seconds
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        if (_agent.velocity == Vector3.zero)
        {
            _anim.SetBool("Walking", false);
        }
        else
        {
            _anim.SetBool("Walking", true);
        }

        //enemy is not chasing player
        if (state == State.NOTCHASING)
        {
            _anim.SetLayerWeight(1, 1f);
            _anim.SetLayerWeight(2, 0f);
            NotChasing();
        }
        //enemy is chasing player
        else
        {
            _agent.stoppingDistance = attackRange;
            //make NPC's destination the player and make it faster
            _agent.destination = player.position;
            _agent.speed = agentSpeed;

            _anim.SetLayerWeight(1, 0f);
            _anim.SetLayerWeight(2, 1f);

            if (!isWithinChaseRange() && state != State.INFCHASE) //give up the chase
            {
                Normal();
            }
        }



    }

    //method to tell enemy what to do when they're not chasing
    public void NotChasing()
    {
        //if enemy does not have to hold position
        if (!waypoints[waypointIndex].holdPosition)
        {
            //set destination of agent based on waypointindex
            _agent.destination = waypoints[waypointIndex].wayPosition;
            _agent.speed = agentSpeed / 2; //lower speed while patrolling
        }
        //enemy has to hold position
        else
        {
            //stop patrol method from cycling index
            CancelInvoke("Patrol");

            //if enemy is close to the waypoint they will hold at and stopholdingtime is 0
            float distance = Vector3.Distance(transform.position, waypoints[waypointIndex].wayPosition);

            if ( (distance < 2) && (stopHoldingTime == 0) )
            {
                _agent.speed = 0;
                stopHoldingTime = Time.time + waypoints[waypointIndex].holdTime;
            }
            //if it is time to stop holding position
            else if ( (distance < 2) && (Time.time >= stopHoldingTime && stopHoldingTime != 0))
            {
                InvokeRepeating("Patrol", 0, patrolTime); //makes enemy patrol every patrolTime seconds
                _agent.destination = waypoints[waypointIndex].wayPosition;
                _agent.speed = agentSpeed / 2; //lower speed while patrolling
                stopHoldingTime = 0;
            }
            //we are on our way to the position we need to hold
            else if (stopHoldingTime == 0)
            {
                //set destination of agent based on waypointindex
                _agent.destination = waypoints[waypointIndex].wayPosition;
                _agent.speed = agentSpeed / 2; //lower speed while patrolling
                stopHoldingTime = 0;
            }
        }
    }

    //method that cyles through the waypoints array
    //this allows the enemy to switch waypoint
    void Patrol()
    {
        //changing waypointIndex based on WHAT IS LIKE AN IF ELSE STATEMENT; setting it equal to 0 if waypointIndex is the LAST waypoint, else set it to waypointIndex + 1 (the next waypoint)
        //what I think this does is cycle through each waypoint, going back to the beginning if the last waypoint is reached
        waypointIndex = waypointIndex == waypoints.Length - 1 ? 0 : waypointIndex + 1;
    }

    void TakeDMG()
    {
        //fill in later
    }


    //method to determine if player is within chase range of the enemy
    public bool isWithinChaseRange()
    {
        bool result = false;
        if ( (player != null) && (Vector3.Distance(transform.position, player.position) < chaseRange) && (state == State.CHASING) )
        {
            result = true;
        }
        else if (state == State.YELLEDAT)
        {
            result = true;
        }
        return result;
    }

    //method to aggro the enemy on to the player
    //shared method
    public void Chase()
    {
        state = State.CHASING;
    }

    //shared method
    public void YelledAt()
    {
        state = State.YELLEDAT;
        Debug.Log("YELLED AT");
    }

    //method to deaggro enemy off the player
    public void Normal()
    {
        state = State.NOTCHASING;
    }

    //method to aggro the enemy on to the player forever
    //shared method
    public void InfChase()
    {
        state = State.INFCHASE;
    }

}
