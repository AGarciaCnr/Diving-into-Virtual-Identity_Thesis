using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskZoneTrigger : MonoBehaviour
{
    [SerializeField] Task task;
    [SerializeField] private AudioClip songTask;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Player") { return; }
        // Debug.Log("Player detectado");
        task.StartTask();
        //if(songTask != null)
        //{
        //    MusicController.instance.NextSong(songTask);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") { return; }
        // Debug.Log("Player detectado");
        task.StopTask();
        //if(songTask != null)
        //{
        //    MusicController.instance.NextSong(null);
        //}
    }
}
