using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = _target.position;
    }
}
