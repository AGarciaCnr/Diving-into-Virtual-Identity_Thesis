using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookController : MonoBehaviour
{
    private Animator anim;
    private float blend;

    private GameObject handL;
    private GameObject handR;
    [SerializeField] private GameObject handLPrefab;
    [SerializeField] private GameObject handBookLPrefab;
    [SerializeField] private GameObject handCoverLPrefab;
    
    [SerializeField] private GameObject handRPrefab;
    [SerializeField] private GameObject handBookRPrefab;
    [SerializeField] private GameObject handCoverRPrefab;

    [SerializeField] private GameObject bookSphere;
    [SerializeField] private GameObject closePoint;
    [SerializeField] private GameObject ballPointL;
    [SerializeField] private GameObject ballPointR;

    [SerializeField] private Task _task;
    
    public float sensibility = 0.1f;

    private XRDirectInteractor interactorL;
    private XRDirectInteractor interactorR;

    // Start is called before the first frame update
    void Start()
    {
        handL = ((GameManager)GameManager.instance).handL;
        handR = ((GameManager)GameManager.instance).handR;
/*        handLPrefab = ((GameManager)GameManager.instance).handL.GetComponentInChildren<Transform>().gameObject;
        handRPrefab = ((GameManager)GameManager.instance).handR.GetComponentInChildren<Transform>().gameObject;*/
        interactorL = handL.GetComponent<XRDirectInteractor>();
        interactorR = handR.GetComponent<XRDirectInteractor>();
        anim = this.GetComponent<Animator>();
        blend = 0;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        anim.SetFloat("open", blend);

        if (interactorR.selectTarget)
        {
            if (interactorR.selectTarget.tag == "Book")
            {
                /*Debug.Log("Right hand is on the book");*/

                handRPrefab.SetActive(false);
                handBookRPrefab.SetActive(true);

                if (interactorL.selectTarget)
                {
                    if (interactorL.selectTarget.tag == "BookSphere")
                    {
                        /*Debug.Log("Left hand is on the sphere");*/

                        handLPrefab.SetActive(false);
                        handCoverLPrefab.SetActive(true);

                        handCoverLPrefab.transform.position = ballPointL.transform.position;

                        blend = ((bookSphere.transform.position.x - closePoint.transform.position.x) / sensibility) - 0.1f;

                        if (blend > 1.0f) { blend = 1.0f; }
                        else if (blend < 0.0f) { blend = 0.0f; }

                        if (blend > 0.7f) { TaskCompleted(); }

                        Debug.Log("blend: " + blend);
                    }
                }
                else
                {
                    bookSphere.transform.position = ballPointL.transform.position;
                    handLPrefab.SetActive(true);
                    handCoverLPrefab.SetActive(false);
                }
            }
        }
        else if (!interactorL.selectTarget)
        {
            blend = 0;
            handRPrefab.SetActive(true);
            handBookRPrefab.SetActive(false);
            handCoverLPrefab.SetActive(false);
        }
        else
        {
            handRPrefab.SetActive(true);
            handBookRPrefab.SetActive(false);
            handCoverLPrefab.SetActive(false);
        }

        if (interactorL.selectTarget)
        {
            if (interactorL.selectTarget.tag == "Book")
            {
                /*Debug.Log("Left hand is on the book");*/

                handLPrefab.SetActive(false);
                handBookLPrefab.SetActive(true);

                if (interactorR.selectTarget)
                {
                    if (interactorR.selectTarget.tag == "BookSphere")
                    {
                        /*Debug.Log("Right hand is on the sphere");*/

                        handRPrefab.SetActive(false);
                        handCoverRPrefab.SetActive(true);

                        handCoverRPrefab.transform.position = ballPointR.transform.position;

                        blend = ((-bookSphere.transform.position.x + closePoint.transform.position.x) / sensibility) - 0.1f;

                        if (blend > 1.0f) { blend = 1.0f; }
                        else if (blend < 0.0f) { blend = 0.0f;}

                        if (blend > 0.7f) { TaskCompleted(); }
                    
                        Debug.Log("blend: " + blend);
                    }
                }
                else
                {
                    bookSphere.transform.position = ballPointR.transform.position;
                    handRPrefab.SetActive(true);
                    handCoverRPrefab.SetActive(false);
                }
            }
        }
        else if (!interactorR.selectTarget)
        {
            blend = 0;
            handLPrefab.SetActive(true);
            handBookLPrefab.SetActive(false);
            handCoverRPrefab.SetActive(false);
        }
        else
        {
            handLPrefab.SetActive(true);
            handBookLPrefab.SetActive(false);
            handCoverRPrefab.SetActive(false);
        }
    }

    void TaskCompleted()
    {
        _task.TaskCompleted();
    }
}