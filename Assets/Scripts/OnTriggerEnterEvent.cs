using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    /*[SerializeField] private string targetTag;*/
    [SerializeField] private UnityEvent onTriggerEvent;

    public GameObject lightImage;

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag != targetTag) { return; }*/
        Debug.Log("Triggered " + this.name);
        if (other.tag != "Player") { return; }
        Debug.Log("Invoke from " + this.name);
        onTriggerEvent.Invoke();
        lightImage.SetActive(true);
    }
}
