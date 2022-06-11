using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plr : MonoBehaviour
{
    public GameObject[][] pinv = new GameObject[10][];

    public Canvas canvas;

    private int itemSelected;

    //for testing purposes
    private int idbSelected;
    private int x = 0;

    private int invnum;
    private bool res;

    public Sprite[] iimgs = new Sprite[2];

    public Button[] invButtons = new Button[10];

    //Collects items on collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stackable") || other.CompareTag("Unstackable"))
        {
            addItem(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    public bool addItem(GameObject go)
    {
        for (int x = 0; x < pinv.Length; x++)
        {
            if (pinv[x] != null)
            {
                //Adds gameobject to player inventory
                if (go.name == pinv[x][0].name)
                {
                    //Debug.Log("The name is same");
                    if (go.CompareTag("Stackable"))
                    {
                        for (int y = 0; y < pinv[x].Length; y++)
                        {
                            if (pinv[x][y] == null)
                            {
                                pinv[x][y] = go;
                                Debug.Log("Stack added slot #" + y + "of array #" + x);
                                return true;
                            }
                            else
                            {
                                Debug.Log("Full at slot " + y);
                            }
                        }


                    }

                }
            }
            else
            {
                //Adds gameobject to empty slot in inventory
                //NOTE: Once more items are implemented, change image into for loop to search iimgs for img with same name as item.
                if (pinv[x] == null)
                {
                    if (go.CompareTag("Stackable"))
                    {
                        Debug.Log("Stack detected");
                        pinv[x] = new GameObject[5];
                        pinv[x][0] = go;
                        invButtons[x].GetComponentInChildren<Image>().sprite = iimgs[0];
                        Debug.Log("Stack added slot #" + x + " with length of " + pinv[x].Length);
                        return true;
                    }
                    if (go.CompareTag("Unstackable"))
                    {
                        pinv[x] = new GameObject[1];
                        pinv[x][0] = go;
                        invButtons[x].GetComponentInChildren<Image>().sprite = iimgs[1];
                        Debug.Log("Nostack added slot #" + x);
                        return true;

                    }
                    Debug.Log("Added slot #" + x);
                    go.SetActive(false);
                    return true;

                }
                else
                {
                    Debug.Log("Parsing Through pinv slot #" + x);
                    if (x == pinv.Length - 1)
                    {
                        Debug.Log("player inventory full");
                        return false;
                    }
                }
            }
        }
        return false;
    }

    //Removes item from inventory
    public void removeItem(int x)
    {
        if (pinv[x][0].CompareTag("Unstackable"))
        {
            pinv[x][0] = null;
            pinv[x] = null;
        }
        else
        {
            //Stackable
            if (pinv[x][0].CompareTag("Stackable"))
            {
                //if there is only one stackable item left in array, removes item from inventory
                if (pinv[x][1] == null && pinv[x][0] != null)
                {
                    pinv[x][0] = null;
                    pinv[x] = null;
                }
                //Removes one item from array
                else
                {
                    for (int i = 0; i < pinv[x].Length; i++)
                    {
                        if (pinv[x][i] == null)
                        {
                            pinv[x][i - 1] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    //Selects item from inventory
    //public GameObject[] selectItem(int arrnum)
    //{
    //    GameObject[] temp = new GameObject[1];
    //    return temp;
    //}
}


