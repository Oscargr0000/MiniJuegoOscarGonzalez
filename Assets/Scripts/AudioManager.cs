using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _musicSource;
    private AudioSource _audiosource;

    public AudioClip[] sound;
    public AudioClip[] music;


    void Start()
    {
        _audiosource = this.GetComponent<AudioSource>(); 
    }

    public void PLayMusic(int musics)
    {
        _musicSource.PlayOneShot(music[musics]);
    }

    public void PLaySound(int sounds)
    {
        _audiosource.PlayOneShot(sound[sounds]);
    }



}
