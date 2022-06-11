using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxSetter : MonoBehaviour
{
    [Tooltip("This is usned for the index of the sprite to be loaded in for the dialogue box")]
    public int currentSprite = 0;
    public Sprite[] backgrounds;

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = backgrounds[currentSprite];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
