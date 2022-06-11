using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the purpose of this script is to decide what an npc does
public class Npc : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    public enum dialogueStatus { prequest, inquest, postquest }
    public dialogueStatus status;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTalking(bool b)
    {
        this._anim.SetBool("Talking", b);
    }

    public bool getTalking()
    {
        return _anim.GetBool("Talking");
    }
}
