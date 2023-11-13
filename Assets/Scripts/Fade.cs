using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    public void FadeOut()
    {
        _animator.Play("FadeOut");
        GameManager.instance.drake.SetActive(false);
    }

    public void FadeIn()
    {
        _animator.Play("FadeIn");
    }


}
