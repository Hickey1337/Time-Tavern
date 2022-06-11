using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

//used to trigger dialogue put this onto what will be speaking
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private VRTK_InteractUse interactUse;
    private VRTK_InteractableObject interactObject;
    private DialogueManager dialogueManager;
    private Npc npc;

    private void Start()
    {
        interactUse = gameObject.GetComponent<VRTK_InteractUse>();
        interactObject = gameObject.GetComponent<VRTK_InteractableObject>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        
        if (gameObject.GetComponent<Npc>() != null)
            npc = gameObject.GetComponent<Npc>();
    }

    private void Update()
    {
        Debug.Log("Is touched = " + interactObject.IsTouched());
        Debug.Log("used = " + interactUse.IsUseButtonPressed());

        if (interactUse.IsUseButtonPressed() && interactObject.IsTouched())
            triggerDialogue();

        if (npc.getTalking() != dialogueManager.animator.GetBool("isOpen"))
            npc.setTalking(dialogueManager.animator.GetBool("isOpen"));
    }

    public void triggerDialogue()
    {
        dialogueManager.startDialogue(dialogue);
    }

    public void nextDialogue()
    {
        dialogueManager.displayNextSentence();
    }
}
