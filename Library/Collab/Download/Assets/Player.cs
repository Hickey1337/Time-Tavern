using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _stamina;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //need to make this damage come from enemy weapons in the f
        if (other.gameObject.tag == "EnemyWeapon")
            _hp--;
    }
}
