using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region patrol
    [Header("Patrol")]
    //objects for patrolling behavior
    [Tooltip("speed of enemy. take note they patrol at half speed")]
    [SerializeField]
    protected float agentSpeed = 5;
    [SerializeField]
    protected float patrolTime = 5f;
    [Tooltip("Waypoints for the enemy to go between")]
    [SerializeField]
    protected Transform[] waypoints;

    [Space(1)]
    #endregion
    #region combat
    [Header("Combat")]
    [Tooltip("Maximum distance between the player and enemy for it to chase.")]
    [SerializeField]
    private float chaseRange = 10f;
    [Tooltip("Distance the enemy attacks the player from")]
    [SerializeField]
    protected float attackRange = 1;
    [Tooltip("ammount of time since last attack")]
    [SerializeField]
    protected float attackTime = 2;

    [Space(1)]
    #endregion
    #region protected
    protected float attackCooldown; //when attack time equals this value the enemy will attack it is randomized by the children scripts
    protected Transform player; //player position
    protected int waypointIndex; //index in waypoint list
    protected enum State { NOTCHASING, CHASING, YELLEDAT, YELLER }; //enumerated type for the enemy current agro level
    protected State state;

    //components
    [Header("Here for a good time not a long time")]
    [SerializeField]
    protected Animator _anim;//make it look for this later -_-
    protected NavMeshAgent _agent;
    #endregion

    void Start()
    {
        state = State.NOTCHASING;
        _agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        //get agent component
        
        if (_agent != null)
        {
            agentSpeed = _agent.speed; //set default speed
        }
        
        //get player x,y,z component
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        //randomize first waypoint
        waypointIndex = Random.Range(0, waypoints.Length);

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
            _agent.stoppingDistance = 0;
            _anim.SetLayerWeight(1, 1f);
            _anim.SetLayerWeight(2, 0f);
            //set destination of agent based on waypointindex
            _agent.destination = waypoints[waypointIndex].position;
            _agent.speed = agentSpeed / 2; //lower speed while patrolling
        }
        //enemy is chasing player
        else if (state == State.CHASING)
        {
            //sets the enemy to attack from the given attack range
            _agent.stoppingDistance = attackRange;
            //set animation layers
            _anim.SetLayerWeight(1, 0f);
            _anim.SetLayerWeight(2, 1f);

            //make NPC's destination the player and make it faster
            _agent.destination = player.position;
            _agent.speed = agentSpeed;

            if (!isWithinChaseRange())
            {
                Normal();
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
}
