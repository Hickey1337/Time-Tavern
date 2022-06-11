using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script is used by the picture frame in the main menu to display the credtis
public class PhotoFrameCredits : MonoBehaviour
{
    //make this non sf later
    [SerializeField]
    private TextMeshProUGUI _tmp;
    [SerializeField]
    private Sprite[] Pictures;
    [SerializeField]
    private string[] Text;


    public int i = 0;
    private SpriteRenderer _sprite;
    

    private void Start()
    {
        if (Pictures.Length != Text.Length)
            Debug.LogError("Photo frame Pictures and Text do not equal the same length");

        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            PreviousIndex();
        if (Input.GetKeyDown(KeyCode.D))
            NextIndex();
    }

    public void NextIndex()
    {
        if (i == Pictures.Length - 1)
            i = 0;
        else
            i++;

        _sprite.sprite = Pictures[i];
        _tmp.text = Text[i];
    }

    public void PreviousIndex()
    {
        if (i == 0)
            i = Pictures.Length - 1;
        else
            i--;

        _sprite.sprite = Pictures[i];
        _tmp.text = Text[i];
    }
}
