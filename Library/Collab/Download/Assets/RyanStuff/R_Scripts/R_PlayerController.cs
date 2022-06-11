using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class R_PlayerController : MonoBehaviour
{
    //private Animator anim;
    private NavMeshAgent agent;

    // Use for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

}
