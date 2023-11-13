using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SwordController : MonoBehaviour
{
    // public GameObject vibrationObject;
    // [SerializeField] private GameObject sparksEffect;

    private bool isRightHanded = true;
    private bool dropped = true;

    private XRGrabInteractable grabInteractable;

    [SerializeField]
    private Transform rightAttach;
    [SerializeField]
    private Transform leftAttach;

    //[SerializeField]
    //private Task _task;

    //private float timer = 0;
    //public float timeToComplete = 5;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

   
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log(other.name);
    //    if (other.gameObject.tag == vibrationObject.tag)
    //    {
    //        timer += Time.deltaTime;
    //        VibrateController(0.1f, 0.5f);
    //        RotateObject();
    //        if(timer >= timeToComplete) { _task.TaskCompleted(); }

    //        // Sparks effect
    //        Vector3 contact = other.ClosestPointOnBounds(transform.position);
    //        sparksEffect.SetActive(true);
    //        sparksEffect.transform.position = contact;
    //        // Instantiate(sparksEffect, contact, Quaternion.identity);
    //    }
    //}

    public void VibrateController(float duration, float amplitude)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(isRightHanded ? XRNode.RightHand : XRNode.LeftHand);
        HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0;
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

    //private void RotateObject()
    //{
    //    vibrationObject.transform.Rotate(Vector3.down * 20 * Time.deltaTime);
    //}

    public void SetHand(HoverEnterEventArgs args)
    {
        if (args.interactor.name == "RightHand Controller" && dropped)
        {
            isRightHanded = true;
            dropped = false;
            grabInteractable.attachTransform = rightAttach;
        }
        else if(args.interactor.name == "LeftHand Controller" && dropped)
        {
            isRightHanded = false;
            dropped = false;
            grabInteractable.attachTransform = leftAttach;
        }
    }

    public void OnSelectExit()
    {
        dropped = true;
    }
}
