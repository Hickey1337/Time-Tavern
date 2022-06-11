using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class R_Yeller : R_Enemy
{
    //unique to yeller
    [SerializeField]
    private R_Enemy nearestAlly; //save position of nearest ally
    [SerializeField]
    private List<R_Enemy> enemyList; //saves all the gameobjects of the yeller's allies

    //detecting allies collider
    //CapsuleCollider capColl;
    public float detectAlly = 60.0f;
    private float yellingDistance = 20.0f;
    //private Transform[] allyList; //yeller gets list of all his allies in the scene

    [SerializeField]
    private float rotateSpeed;

    protected override void Start()
    {
        base.Start();
        state = State.YELLER;

        rotateSpeed = 1000f;
        enemyList = new List<R_Enemy>();

        UpdateListOfAllies();
        UpdateNearestAlly();
    }

    // Update is called once per frame
    public override void Update()
    {
        UpdateListOfAllies();
        UpdateNearestAlly();

        //yeller is in normal state
        if (state == State.YELLER)
        {
            base.NotChasing();
        }
        //player has walked into line of sight of yeller
        else
        {
            if (nearestAlly != null)
            {
                float distanceToAlly = Vector3.Distance(transform.position, nearestAlly.transform.position);
                Debug.Log("distanceToAlly" + distanceToAlly);

                //if yeller can sense an ally
                if (distanceToAlly < detectAlly)
                {
                    Debug.Log("inrange");
                    _anim.SetBool("Yelling", true);
                    _agent.destination = nearestAlly.transform.position; //go to ally
                    _agent.speed = agentSpeed * 2;

                    //if yeller can yell to that ally yet
                    if (distanceToAlly < yellingDistance)
                    {
                        nearestAlly.YelledAt();
                        UpdateListOfAllies();
                        UpdateNearestAlly();
                    }
                }
                else
                {
                    //no ally close enough - start freakin out
                    //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                    _agent.speed = 0;
                }
            }
            else
            {
                // no allies nearby - start freakin out
                //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                _agent.speed = 0;
            }

        }


    }//end of update

    //update yeller's list of allies who are not chasing
    private void UpdateListOfAllies()
    {
        enemyList.Clear();
        R_Enemy[] helper = FindObjectsOfType( typeof(R_Enemy) ) as R_Enemy[];

        //Debug.Log(helper);

        for (int i=0; i < helper.Length; i++)
        {
            R_Enemy currentEnemy = helper[i];
            
            if (currentEnemy != null && currentEnemy.state == State.NOTCHASING)
            {
                enemyList.Add(currentEnemy);
            }
        }

    }

    //tell the yeller who the new nearest ally is
    private void UpdateNearestAlly()
    {
        nearestAlly = GetClosestAllyObject(enemyList); 
    }
   

    //taken from https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
    //return closest enemy object of closest ally to yeller
    private R_Enemy GetClosestAllyObject(List<R_Enemy> allies)
    {
        R_Enemy closestAlly = null; //transform to return
        float smallestDistance = 1000.0f; //big number that enemy should be lower than
        Vector3 currentPosition = transform.position; //position of yeller

        foreach (R_Enemy g in allies)
        {
            float distance = Vector3.Distance(g.transform.position, currentPosition);

            if (distance < smallestDistance) //if distance to ally is less than smallest
            {
                    closestAlly = g; //set new closest ally
                    smallestDistance = distance; //update smallest distance
            }
        }

        return closestAlly; //return the object
    }

}

