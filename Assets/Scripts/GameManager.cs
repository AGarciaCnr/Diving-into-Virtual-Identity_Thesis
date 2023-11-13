using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Task> tasks;
    [SerializeField] private AudioClip audioIntroduction;
    [SerializeField] private AudioClip taskFailureAudio;
    [SerializeField] private AudioClip taskCompletedAudio;
    [SerializeField] private AudioClip allTasksCompletedAudio;
    [SerializeField] private UnityEvent allTasksEvent;


    private AudioSource audioSource;
    [HideInInspector] public bool drakeIntro = false;
    [SerializeField] private Animator _animatorDrake;

    private Task _currentTask;
    private Task _reproducingTask;

    public GameObject handL;
    public GameObject handLModel;
    public GameObject handR;
    public GameObject handRModel;

    public GameObject drake;
    public GameObject initialLetter;
    [SerializeField] private GameObject drakePapyrus;

    private float outlinePapyrusTimer;
    private bool papyrusSeen = false;
    public bool letterActive = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _currentTask = null;
        // InvokeDrake();
    }

    private void Update()
    {
        if(initialLetter.GetComponent<Outline>().enabled == false && letterActive)
        {
            outlinePapyrusTimer += Time.deltaTime;
            if(outlinePapyrusTimer >= 6.0f && !papyrusSeen)
            {
                drakePapyrus.GetComponent<Outline>().enabled = true;
                papyrusSeen = true;
            }
        }
    }

    public void InvokeDrake()
    {
        Debug.Log("DRAGON INVOCADO");
        drake.SetActive(true);
        initialLetter.GetComponent<Outline>().enabled = false;
        drakePapyrus.GetComponent<Outline>().enabled = false;
        StartCoroutine(IntroductionDrake());
    }

    IEnumerator IntroductionDrake()
    {
        yield return new WaitForSeconds(1.0f);
        audioSource.clip = audioIntroduction;
        audioSource.Play();
        _animatorDrake.SetTrigger("presentation");

        yield return new WaitForSeconds(audioIntroduction.length);
        drakeIntro = true;
        foreach (Task _task in tasks)
        {
            _task.portal.SetActive(true);
        }
    }

    public void StartTask(Task t)
    {
        if (!drakeIntro) { 
            // Debug.Log("La intro del dragón no ha acabado"); 
            return; 
        }
        if(t == _currentTask) { Debug.Log("Misma task"); return; }
        if(_reproducingTask != null) { Debug.Log("Se está reproduciendo una task ya"); return; }

        _currentTask = t;
        drake.transform.LookAt(new Vector3(handL.transform.position.x, 0.0f, handL.transform.position.z));
        StartCoroutine(ReproduceTask());
    }

    IEnumerator ReproduceTask()
    {
        _reproducingTask = _currentTask;
        Debug.Log("COMENZANDO TASK");
        _reproducingTask.state = TaskState.REPRODUCING;
        audioSource.clip = _reproducingTask.audioClip;
        audioSource.Play();
        _animatorDrake.SetTrigger(_currentTask.animationDrakeString);

        yield return new WaitForSeconds(_currentTask.audioClip.length);
        _reproducingTask.state = TaskState.DOING;
        _reproducingTask = null;
    }

    public void StopTask()
    {
        if (_currentTask == null) { return; }
        _currentTask = null;
        // StopCoroutine(ReproduceTask());
    }

    public void VerifyAllCompletedTask()
    {
        foreach(Task t in tasks)
        {
            if(t.state != TaskState.COMPLETED) { return; }
        }
        StartCoroutine(EndExperience());
    }

    IEnumerator EndExperience()
    {
        audioSource.clip = allTasksCompletedAudio;
        audioSource.Play();
        _animatorDrake.SetTrigger("allCompleted");

        yield return new WaitForSeconds(audioSource.clip.length);
        allTasksEvent.Invoke();

        yield return new WaitForSeconds(3.0f);
        Application.Quit();
    }

    public void FailInTask()
    {
        if (!drakeIntro) { Debug.Log("La intro del dragón no ha acabado"); return; }
        audioSource.clip = taskFailureAudio;
        audioSource.Play();
        _animatorDrake.SetTrigger("fail");
    }

    public void TaskCompletedAudio()
    {
        audioSource.clip = taskCompletedAudio;
        audioSource.Play();
        _animatorDrake.SetTrigger("taskCompleted");
    }


}
