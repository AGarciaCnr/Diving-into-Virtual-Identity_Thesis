using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : Singleton<MusicController>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip introSong;
    [SerializeField] private AudioClip idleSong;


    [SerializeField] private AudioClip nextSong = null;

    private void Start()
    {

        StartCoroutine(ReproduceSong());
    }


    IEnumerator ReproduceSong()
    {
        audioSource.clip = introSong;
        audioSource.Play();
        yield return new WaitForSeconds(introSong.length);

        while (true)
        {
            audioSource.clip = (nextSong == null) ? idleSong : nextSong;
            audioSource.Play();
            
            yield return new WaitForSeconds(audioSource.clip.length);
            
        }

    }

    public void NextSong(AudioClip nextSong)
    {
        this.nextSong = nextSong;
        //if(nextSong == null)
        //{
        //    Debug.Log("No hay siguiente cancion");
        //}
        //else
        //{
        //    Debug.Log("Siguiente cancion: " + nextSong.name);
        //}
    }
}
