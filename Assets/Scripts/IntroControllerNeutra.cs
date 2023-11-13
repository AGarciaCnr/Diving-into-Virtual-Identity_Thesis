using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroControllerNeutra : MonoBehaviour
{
    [SerializeField] private Fade _fade;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _musicController;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Introduction());
    }

   
    IEnumerator Introduction()
    {
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);
        
        _fade.FadeIn();
        GameManager.instance.initialLetter.GetComponent<Outline>().enabled = true;
        GameManager.instance.letterActive = true;
        _musicController.SetActive(true);

    }
}
