using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTask : Task
{
    [SerializeField] private GameObject _sword;

    private bool outlined = false;

    public override void StartTask()
    {
        base.StartTask();
        Debug.Log("hijo");
        if (!outlined)
        {
            _sword.GetComponent<Outline>().enabled = true;
            outlined = true;
        }
    }
}
