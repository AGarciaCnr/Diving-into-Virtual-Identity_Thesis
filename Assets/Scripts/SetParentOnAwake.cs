using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentOnAwake : MonoBehaviour
{
    [SerializeField] private Transform _newParent;
    private void Awake()
    {
        transform.SetParent(_newParent);
    }
}
