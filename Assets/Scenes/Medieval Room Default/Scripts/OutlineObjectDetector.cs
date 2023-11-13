using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class OutlineObjectDetector : MonoBehaviour
{
    public float maxDistance = 10f;
    public float gazeTime = 0.5f;
    private float timer; 

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * maxDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Estas mirando a " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<Outline>())
            {
                timer += Time.deltaTime;

                if (timer >= gazeTime)
                {
                    hit.collider.gameObject.GetComponent<Outline>().enabled = false;
                    timer = 0.0f;
                }
            }
        }
    }
}