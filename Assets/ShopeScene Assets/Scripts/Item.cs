using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item:ScriptableObject
{
    public string itemName;
    public Sprite img;
    public bool Stackable;
    //public int itemcount;
    //public int maxcount;
    //public GameObject prefab;
}
