using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : InteractButton
{
    [Tooltip("Targeted game object that the button will make play an action")]
    [SerializeField]
    private GameObject target;
    public GameObject player;

    //enumerate actions
    enum Action {Animate, Activate, Text, Warp};
    [SerializeField]
    private Action action = Action.Animate;
    //Only needed for playing an animation
    [Tooltip("Used to Animate the Target")]
    [SerializeField]
    private Animation animation;

    void Start()
    {
        //This is used so it can properly warp the player
        if(action == Action.Warp)
            player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (getPressed())
        {
            //play action here
            if (action == Action.Animate)
            {
                PlayAnimation("Door Opening");
            }
            else if (action == Action.Activate)
            {

            }
            else if (action == Action.Text)
            {

            }
            else if (action == Action.Warp)
            {
                Vector3 warpPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
                player.transform.position = warpPos;
            }
        }
    }

    private void PlayAnimation(string AnimName)
    {
        target.GetComponent<Animation>().Play(AnimName);
    }

    private void Activate(GameObject GO)
    {
        GO.SetActive(true);
    }
}