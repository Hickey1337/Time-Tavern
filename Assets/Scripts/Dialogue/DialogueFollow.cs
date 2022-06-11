using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script used for having the dialogue follow the players line of sight and then suspend it on the spot
public class DialogueFollow : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    private Animator _anim;
    [SerializeField]
    private GameObject PlayerFace;

    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.GetBool("isOpen") == true)
        {
            //looks at and inverts
            transform.LookAt(PlayerFace.transform);
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(-rot.x, rot.y + 180, -rot.z);
            transform.rotation = Quaternion.Euler(rot);

            //detaches from parent and keeps orignial position
            Vector3 temp = transform.position;
            transform.SetParent(null);
            transform.position = temp;
        }
        else if (_anim.GetBool("isOpen") != true && transform.parent != parent)
        {
            //the animation ends when the local scalse for x and y are 0
            //this is a work around for finding when the 1 frame animation ends
            if (gameObject.transform.localScale.x == 0)
            {
                transform.SetParent(parent);
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
