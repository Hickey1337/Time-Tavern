using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//object to pass intot he dialogue manager
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
