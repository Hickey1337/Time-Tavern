using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : InteractButton
{
    private bool playing;//this is a place holder for not starting multiple times will be using audio manager eventually

    [Tooltip("Seconds to wait before spawning the door button")]
    [SerializeField]
    private float seconds;

    public GameObject doorPodium;
    private GameObject emiter;

    private void Start()
    {
        emiter = transform.Find("Audio Emmiter").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (getPressed() && !playing)
        {
            Debug.Log("Tutorial pressed");
            playing = true;

            //play audio here
            emiter.GetComponent<AudioSource>().Play();

            //set courutine to spawn the button
            StartCoroutine(spawnDoorButton());
        }
    }

    IEnumerator spawnDoorButton()
    {
        yield return new WaitForSeconds(seconds);
        doorPodium.SetActive(true);
    }
}
