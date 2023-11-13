using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroControllerMagica: MonoBehaviour
{
    [SerializeField] private Fade _fade;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioSource> _audioSourcesToActivate;
    [SerializeField] private VideoPlayer _videoKing;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Introduction());
    }

   
    IEnumerator Introduction()
    {
        _videoKing.Prepare();
        while (!_videoKing.isPrepared)
        {

            yield return null;
        }

        _fade.FadeIn();
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);

        foreach(AudioSource source in _audioSourcesToActivate)
        {
            source.enabled = true;
        }

        GameManager.instance.initialLetter.GetComponent<Outline>().enabled = true;
        GameManager.instance.letterActive = true;
    }
}
