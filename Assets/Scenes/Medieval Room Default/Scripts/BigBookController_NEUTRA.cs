using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BigBookController_NEUTRA : MonoBehaviour
{
    private Animator anim;
    private float blend;

    private bool dropped = true;

    public float sensibility;

    [SerializeField] private GameObject grabSphere;
    [SerializeField] private GameObject closePoint;
    [SerializeField] private GameObject droppedSpherePoint;
    [SerializeField] private GameObject handLBookModel;
    [SerializeField] private GameObject handRBookModel;

    private GameObject HandModelToShow;
    private GameObject HandModelToHide;

    [SerializeField] private Task _task;

    void Start()
    {
        sensibility = 0.5f;
        anim = this.GetComponent<Animator>();
        blend = 0.0f;
    }

    void Update()
    {
        anim.SetFloat("open", blend);
        if (!dropped)
        {
            blend = (grabSphere.transform.position.z - closePoint.transform.position.z) / sensibility;
            HandModelToShow.transform.position = new Vector3(droppedSpherePoint.transform.position.x, droppedSpherePoint.transform.position.y, droppedSpherePoint.transform.position.z);
        }

        if (blend > 1.0f) { blend = 1.0f; }
        else if (blend < 0.0f) { blend = 0.0f; }

        if (blend > 0.7f) { _task.TaskCompleted(); }
    }

    public void SetHand(HoverEnterEventArgs args)
    {
        if (args.interactor.name == "RightHand Controller" && dropped)
        {
            HandModelToShow = handRBookModel.gameObject;
            HandModelToHide = ((GameManager)GameManager.instance).handRModel.gameObject;
            dropped = false;
        }
        else if (args.interactor.name == "LeftHand Controller" && dropped)
        {
            HandModelToShow = handLBookModel.gameObject;
            HandModelToHide = ((GameManager)GameManager.instance).handLModel.gameObject;
            dropped = false;
        }
    }

    public void OnSelectEntered()
    {
        HandModelToShow.SetActive(true);
        HandModelToHide.SetActive(false);
    }

    public void OnSelectExit()
    {
        dropped = true;
        grabSphere.gameObject.transform.position = droppedSpherePoint.gameObject.transform.position;
        HandModelToShow.SetActive(false);
        HandModelToHide.SetActive(true);
    }
}
