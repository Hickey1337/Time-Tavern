using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClubHolder : Enemy
{
    private void Attack()
    {
        //resets attack timer
        attackTime = 0;
        //picks a random attakc time between 3 to 7 seconds
        attackCooldown = Random.Range(3, 7);
    }

    public override void Update()
    {
        base.Update();

        if (base.isWithinChaseRange())
        {
            _anim.SetBool("Attacking", false);
            //Debug.Log("IN range");
            if (attackTime >= attackCooldown)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    Attack();
                    _anim.SetBool("Attacking", true);
                }
            }
            attackTime += Time.deltaTime;
        }
    }
}
