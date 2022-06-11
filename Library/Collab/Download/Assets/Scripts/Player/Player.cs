using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _stamina;
    [SerializeField]
    private TextMeshPro tmp;


    // Start is called before the first frame update
    void Start()
    {
        tmp.text = _hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
            return;
            //warp back to the hub area
    }

    private void takeDMG()
    {
        _hp--;
        tmp.text = _hp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        //need to make this damage come from enemy weapons in the f
        if (other.gameObject.tag == "EnemyWeapon")
            _hp--;
    }
}
