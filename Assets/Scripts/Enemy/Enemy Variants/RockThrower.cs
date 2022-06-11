using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : Enemy
{
    [Space(1)]
    [Header("RockThrower")]
    [Tooltip("Item to be thrown")]
    [SerializeField]
    private GameObject throwable;

    //make this look for it in children eventually
    [SerializeField]
    protected Transform spawnPoint;

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

        if ( base.isWithinChaseRange())
        {
            _anim.SetBool("Attacking", false);
            if (attackTime >= attackCooldown) //if it's time to throw rock
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    Attack(); //throw rock
                    _anim.SetBool("Attacking", true);
                }
                transform.LookAt(player);
            }
            attackTime += Time.deltaTime;
            transform.LookAt(player);
        }
    }
}
