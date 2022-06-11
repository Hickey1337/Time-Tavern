using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class R_MouseManager : MonoBehaviour
{
    //know what objects are clickable
    public LayerMask clickableLayer;

    //Swap cursors per object
    public Texture2D pointer; //normal pointer
    public Texture2D target; //pointer for clickable objects
    public Texture2D doorway; //cursor for doorways
    public Texture2D combat; //cursor for combat actions

    public EventVector3 OnClickEnvironment; //position of mouse click

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Raycast.Debug();

        //if over something clickable
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 600, clickableLayer.value))
        {
            //Debug.Log("HIT");
            bool door = false;
            bool item = false;

            //if hovering over a door
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }

            //if hovering over item
            else if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }

            //over anything else clickable
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0)) //if left mouse button clicked
            {
                if (door) //if door is clicked
                {
                    Transform doorway = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                }
                else if (item) // if item is clicked
                {
                    Transform ourItem = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(ourItem.position);
                    Debug.Log("ITEM");
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }
        }
        //not over something clickable
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

//create EventVector3 type
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
