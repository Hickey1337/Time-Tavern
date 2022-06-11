using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTickFloat : MonoBehaviour
{
    private TextMeshPro _tmp;
    private Vector3 _newPosition;

    void Start()
    {
        _tmp = gameObject.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (_tmp.alpha <= 0)
            Destroy(gameObject);
        else
            _tmp.alpha -= .005f;
        _newPosition = transform.position;
        _newPosition.x += Mathf.Sin(Time.time*3f) * Time.deltaTime;
        _newPosition.y += .003f;
        transform.position = _newPosition;
    }
}
