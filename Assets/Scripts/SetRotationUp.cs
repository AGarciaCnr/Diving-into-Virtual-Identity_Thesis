using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotationUp : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }


    // Update is called once per frame
    void Update()
    {
        _transform.LookAt(_transform.position + Vector3.forward);
    }
}