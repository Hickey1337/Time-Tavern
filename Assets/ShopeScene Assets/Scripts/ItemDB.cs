//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ItemDB : MonoBehaviour
//{
//    //public List<Item> itemDatabase = new List<Item>();

//    public plr p;

//    private int invnum;
//    private bool res;

//    public Sprite[] iimgs = new Sprite[2];

//    public GameObject gameobj;
//    //public GameObject[][] pinv = new GameObject[10][];

//    public Button[] invButtons = new Button[10];

//    //Adds item to inventory
//    public void addItem(GameObject go)
//    {

//        for (int x = 0; x < p.pinv.Length; x++)
//        {
//            if (p.pinv[x] != null)
//            {
//                //Adds gameobject to player inventory
//                if (go.name.Equals(p.pinv[x][0].name))
//                {
//                    if (go.CompareTag("Stackable"))
//                    {
//                        for (int y = 0; y < p.pinv[x].Length; y++)
//                        {
//                            p.pinv[x] = new GameObject[5];
//                            p.pinv[x][y] = go;
//                            if(p.pinv[x][y] != null)
//                            {
//                                Debug.Log("Full");
//                            }
//                            //p.pdisp[x] = go;
//                            //invButtons[x].GetComponentInChildren<Image>().sprite = iimgs[0];
//                            //p.pinv[x] = new GameObject[5];
//                            //p.pinv[x][0] = go;
//                            //p.pdisp[x] = go;
//                            break;
//                        }


//                    }
//                }
//            }

//            //Adds gameobject to empty slot in inventory
//            //NOTE: Once more items are implemented, change image into for loop to search iimgs for img with same name as item.
//            if (p.pinv[x] == null)
//            {
//                if (go.CompareTag("Stackable"))
//                {
//                    p.pinv[x] = new GameObject[5];
//                    p.pinv[x][0] = go;
//                    p.pdisp[x] = go;
//                    invButtons[x].GetComponentInChildren<Image>().sprite = iimgs[0];


//                }
//                if (go.CompareTag("Unstackable"))
//                {
//                    p.pinv[x] = new GameObject[1];
//                    p.pinv[x][0] = go;
//                    p.pdisp[x] = go;
//                    invButtons[x].GetComponentInChildren<Image>().sprite = iimgs[1];

//                }
//                Debug.Log("Added slot #" + x);
//                go.SetActive(false);
//                break;
//            }
//            else
//            {
//                Debug.Log("Parsing Through pinv slot #" + x);
//                if (x == p.pinv.Length - 1)
//                {
//                    Debug.Log("player inventory full");
//                }
//            }
//        }
//    }

//    //Removes item from inventory (Do by end of 3/31)
//    public void removeItem(int x)
//    {
//        if(p.pinv[x][0].CompareTag("Unstackable"))
//        {
//            p.pinv[x][0] = null;
//            p.pinv[x] = null;
//            p.pdisp[x] = null;

//        }
//        else
//        {
//            //Stackable
//            if (p.pinv[x][0].CompareTag("Stackable"))
//            {
//                //if there is only one stackable item left in array, removes item from inventory
//                if(p.pinv[x][1] == null && p.pinv[x][0] != null)
//                {
//                    p.pinv[x][0] = null;
//                    p.pinv[x] = null;
//                    p.pdisp[x] = null;
//                }
//                //Removes one item from array
//                else
//                {
//                    for(int i = 0; i < p.pinv[x].Length;i++)
//                    {
//                        if(p.pinv[x][i] == null)
//                        {
//                            p.pinv[x][i - 1] = null;
//                            break;
//                        }
//                    }
//                }
//            }
//        }
//    }

//    //Selects item from inventory (Do by end of 4/1)
//    public GameObject[] selectItem(int arrnum)
//    {
//        GameObject[] temp = new GameObject[1];
//        return temp;
//    }
//}

////Adds Item to inventory
//public void AddItem(int iID, plr p)
//    {
//        //Loops through each item in itemDatabase
//        foreach(var item in itemDatabase)
//        {
//            if(item.id == iID)
//            {
//                Debug.Log("Yes, Parsing");
                
//                //Loops through each item in player's inventory
//                foreach (var item2 in p.inventory)
//                {
//                    //Parses if inventory at array slot empty
//                    if(p.inventory[invnum].id != -1 && invnum < p.inventory.Length)
//                    {
//                        invnum++;
//                        Debug.Log("Parsing #" + invnum);
//                    }
//                    else
//                    {
//                        //Adds item to inventory
//                        p.inventory[invnum] = item;
//                        Debug.Log("Successfully added to slot " + invnum);
//                        invnum++;
//                        break;
//                    }
//                }
//            }
//            Debug.Log("Not Added");
//        }
//    }

//    public bool addStackable(plr p)
//{
//    foreach (var item in itemDatabase)
//    {
//        if (p.inventory[invnum].id == item.id && p.inventory[invnum].itemcount < item.maxcount)
//        {
//            res = true;
//            p.inventory[invnum] = item;
//            p.inventory[invnum].itemcount++;
//            Debug.Log("Successfully added to slot " + invnum);
//            invnum = 0;
//        }
//        else
//        {
//            res = false;
//        }

//    }
//    return res;
//}

//private int resetNum(int x)
//{
//    x = 0;
//    return x;
//}

//public void RemoveItem(int iID, int plrinvnum, plr p)
//{
//    //foreach (var item in itemDatabase)
//    //{
//    p.inventory[plrinvnum] = null;
//    p.inventory[plrinvnum].id = -1;
//    //}

//    //foreach (var item in itemDatabase)
//    //{
//    //    if (item.id == iID)
//    //    {
//    //        Debug.Log("yes");

//    //        Debug.Log("Parsing");

//    //        foreach (var item2 in p.inventory)
//    //        {
//    //            if (p.inventory[invnum].id != -1 && invnum <= p.inventory.Length)
//    //            {
//    //                invnum++;
//    //                Debug.Log("Parsing #" + invnum);
//    //            }
//    //            else
//    //            {
//    //                p.inventory[invnum] = item;
//    //                Debug.Log("Successfully added to slot " + invnum);
//    //                invnum = 0;
//    //            }
//    //            if (invnum >= p.inventory.Length)
//    //            {
//    //                Debug.Log("inventory full");
//    //            }
//    //        }
//    //    }
//    //    Debug.Log("no");
//    //}
//}
//}
//public void AddItem(int iID, plr p)
//{
//    resetNum(invnum);
//    foreach (var item in itemDatabase)
//    {
//        if (item.id == iID)
//        {
//            Debug.Log("yes");
//            Debug.Log("Parsing");

//            foreach (var item2 in p.inventory)
//            {
//                //Checks for stackable items already in inventory
//                if (p.inventory[invnum].id == item.id)
//                {
//                    if (item.Stackable && p.inventory[invnum].itemcount < p.inventory[invnum].maxcount)
//                    {
//                        addStackable(p);
//                        break;
//                    }
//                    if (!item.Stackable)
//                    {
//                        Debug.Log("Item already in inventory");
//                        break;
//                    }
//                }

//                //Checks for empty inventory slots
//                if (p.inventory[invnum].id == -1)
//                {
//                    p.inventory[invnum] = item;
//                    p.inventory[invnum].itemcount++;
//                    Debug.Log("Successfully added to slot " + invnum);
//                    resetNum(invnum);
//                    break;
//                }

//                //    if (p.inventory[invnum] == null && invnum <= p.inventory.Length)
//                //{
//                //    if (!addStackable(p))
//                //    {
//                //        invnum++;
//                //        Debug.Log("Parsing #" + invnum);
//                //    }
//                //    else
//                //    {
//                //        break;
//                //    }
//                //}
//                //else
//                //{
//                //p.inventory[invnum] = item;
//                //p.inventory[invnum].itemcount++;
//                //Debug.Log("Successfully added to slot " + invnum);
//                //resetNum(invnum);
//                //break;
//                //}
//                if (invnum >= p.inventory.Length)
//                {
//                    Debug.Log("inventory full");
//                }
//            }
//        }
//        Debug.Log("no");
//    }
//}