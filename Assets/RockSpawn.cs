using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public R_RockThrower rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = transform.parent.GetComponent<R_RockThrower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        Instantiate(rt.throwable, rt.spawnPoint.transform.position, transform.parent.localRotation);
    }
}
