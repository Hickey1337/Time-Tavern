using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//SHOULD PROBABLY BE BASE OF ALL OTHER ENEMY TYPES
public class R_ClubHolder : R_Enemy
{
    private float[] numbers = new float [3];

    private void Start()
    {
        numbers[0] = 0;
        numbers[1] = 0.5f;
        numbers[2] = 1f;
    }

    //could also call swing club
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
                    int randomIndex = Random.Range(0, 3);
                    float randVal = numbers[randomIndex];
                    Attack();
                    _anim.SetFloat("Swing", randVal);
                    _anim.SetBool("Attacking", true);
                }
            }
            attackTime += Time.deltaTime;
        }
    }
}
