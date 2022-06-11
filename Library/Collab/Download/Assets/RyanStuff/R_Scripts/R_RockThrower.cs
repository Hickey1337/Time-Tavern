using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_RockThrower : R_Enemy
{
    public GameObject throwable;
    public Transform spawnPoint;

    //public float throwCooldown = 3; //cooldown of throwing rocks
    //private float nextThrowTime = 0; //time when thrower can throw next



    public void Attack()
    {
        //resets attack timer
        attackTime = 0;
        //picks a random attakc time between 2 to 5 seconds
        attackCooldown = Random.Range(2, 5);
        //create rock
        Instantiate(throwable, spawnPoint.transform.position, transform.localRotation);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if ( base.isWithinChaseRange() )
        {
            _anim.SetBool("Attacking", false);
            if (attackTime >= attackCooldown) //if it's time to throw rock
            {
                Debug.Log("it's time!");
                //put attack range back in
                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    Debug.Log("attack!");
                    agentSpeed = 0;
                    Attack(); //throw rock
                    _anim.SetBool("Attacking", true);
                }
                //transform.LookAt(player);
            }
            attackTime += Time.deltaTime;
            //transform.LookAt(player);
            agentSpeed = 5;
        }
    }
}
