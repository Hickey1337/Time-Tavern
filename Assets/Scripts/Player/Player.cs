using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _stamina;
    [SerializeField]
    private TextMeshPro tmp;

    private float _hitTime = 1f;//Ammouunt of time before next hit is allowed
    private float _hitTimer = 0;//Tracks time since last hit
    private bool _canHit = true;//allows the palyer to hit enemy


    // Start is called before the first frame update
    void Start()
    {
        tmp.text = _hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
            SceneManager.LoadScene("ShopScene");
        //warp back to the hub area

        _hitTimer += Time.deltaTime;

        if (_hitTimer > _hitTime)
            _canHit = true;
    }

    private void takeDMG()
    {
        _hp--;
        tmp.text = _hp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyWeapon")
            if (_canHit == true)
            {
                _canHit = false;
                _hitTimer = 0;
                takeDMG();
            }
    }

}

