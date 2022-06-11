using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Used for handling enemys health
public class EnemyStats : MonoBehaviour
{
    public float hp = 100;//Current health
    public GameObject Damage;
    public Transform TextSpawn;
    private float _HeadDmg = 3f;//Damgage for the head
    private float _BodyDmg = 2f;//Damage for the body
    private float _ArmDmg = 1f;//Damage for the arm
    private float _LegDmg = 1f;//Damage for the leg
    private float _hitTime = 0.5f;//Ammouunt of time before next hit is allowed
    private float _hitTimer = 0;//Tracks time since last hit
    private bool _canHit = true;//allows the palyer to hit enemy

    // Update is called once per frame
    void Update()
    {
        //Increment the hit timer
        _hitTimer += Time.deltaTime;

        if (_hitTimer > _hitTime)
            _canHit = true;

        if (hp <= 0)
            Destroy(transform.parent.gameObject);

    }


    private void OnCollisionEnter(Collision collision)
    {
        //Cehcks to see if it has previously collided this frame
        if (_canHit == true)
        {
            _canHit = false;
            _hitTimer = 0;

            foreach (ContactPoint contact in collision.contacts)
            {
                //Checks to see that it was hit by a blade
                if (contact.otherCollider.tag == "Blade")
                {
                    //checks for body part hit
                    if (contact.thisCollider.tag == "Head")
                    {
                        TakeDamage(_HeadDmg);
                    }
                    else if (contact.thisCollider.tag == "Body")
                    {
                        TakeDamage(_BodyDmg);
                    }
                    else if (contact.thisCollider.tag == "Arm")
                    {
                        TakeDamage(_ArmDmg);
                    }
                    else if (contact.thisCollider.tag == "Legs")
                    {
                        TakeDamage(_LegDmg);
                    }
                }
            }
        }
    }

    //spawns in text, sets the text and applys damage to the enemy
    private void TakeDamage(float dmg)
    {
        GameObject Text = Damage;
        Text.GetComponent<TextMeshPro>().text = dmg.ToString();
        hp -= dmg;
        Instantiate(Text, TextSpawn);
    }

}
