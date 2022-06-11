using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool invShown = false;
    public GameObject pauseUI;

    //public GameObject inventoryUI;
    
    //public Button resumeButton;
    //public Button inventoryButton;
    //public Button quitButton;


    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //If resume button is clicked
        //resumeButton.GetComponent<Button>().onClick.AddListener(() => resumeGame());

        /*
        //If inventory button is clicked
        inventoryButton.GetComponent<Button>().onClick.AddListener(() =>
        {

            //Toggles if Inventory UI is shown or not
            if (!invShown)
            {
                showInventory();
            }
            else
            {
                hideInventory();
            }
        });
        */

        //If quit button is clicked
        //quitButton.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
    }

    //pauses game
    public void pauseGame()
    {
        //anim.SetBool("isOpen", true);
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //resumes game
    public void resumeGame()
    {
        //anim.SetBool("isOpen", false);
        pauseUI.SetActive(false);
        //hideInventory();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void exitGame()
    {

    }

    /*
    //Shows inventory
    void showInventory()
    {
        inventoryUI.SetActive(true);
        invShown = true;
    }

    //Hides inventory
    void hideInventory()
    {
        inventoryUI.SetActive(false);
        invShown = false;
    }
    */
}
