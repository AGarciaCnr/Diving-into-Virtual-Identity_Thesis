using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCorkEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEvent;
    [SerializeField] private GameObject cork;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != cork.tag) { return; }
        onTriggerEvent.Invoke();
    }
}
