using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    public GameObject CameraRig;
    public GameObject spawn;
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (GameObject.Find("SpawnPoint") != null)
        {
            spawn = GameObject.Find("SpawnPoint");
            transform.position = spawn.transform.position;
            //transform.rotation = GameObject.Find("SpawnPoint").transform.rotation;
            //meraRig.transform.position = Vector3.zero;
            //meraRig.transform.rotation = Quaternion.identity;
        }
        //else
            //Debug.LogError("no SpawnPoint gameobject");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn == null)
        {
            spawn = GameObject.Find("SpawnPoint");
            transform.position = spawn.transform.position;
        }
    }
}
