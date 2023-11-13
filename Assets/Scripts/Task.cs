using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TaskType
{
    DRAKE,
    POTIONS,
    SWORD,
    BOOK,
};

public enum TaskState
{
    WITHOUT_STARTING,
    REPRODUCING,
    DOING,
    COMPLETED
}

public class Task : MonoBehaviour
{
    public TaskType testType;
    public AudioClip audioClip;
    public TaskState state = TaskState.WITHOUT_STARTING;
    // [SerializeField] private float _timeForNewReproduce = 120;
    public string animationDrakeString;

    public GameObject portal;

    private GameManager _gameManager;

    // public float _timeSinceLastPlay = 0;

    private void Update()
    {
        // if(state != TaskState.DOING) { return; }

    }

    private void Awake()
    {
        _gameManager = GameManager.instance;
    }

    public virtual void StartTask()
    {
        if (!_gameManager.drakeIntro) { return; }
        if (state == TaskState.COMPLETED) { return; }
        portal.SetActive(false);
        _gameManager.StartTask(this);
        Debug.Log("padre");
    }

    public void TaskCompleted()
    {
        if(!_gameManager.drakeIntro) { return; }
        if(state == TaskState.COMPLETED) { return; }
        Debug.Log("SE HA COMPLETADO CORRECTAMENTE LA TAREA " + testType);
        state = TaskState.COMPLETED;
        _gameManager.TaskCompletedAudio();
        _gameManager.VerifyAllCompletedTask();
    }

    public void StopTask()
    {
        if (!_gameManager.drakeIntro) { return; }
        if (state == TaskState.COMPLETED) { return; }
        // state = TaskState.WITHOUT_STARTING;
        _gameManager.StopTask();
    }

}
