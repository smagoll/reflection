using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sfx;
    
    public AudioClip _throw;
    public AudioClip _rebound;
    public AudioClip _hit;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfx.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        music.clip = audioClip;
        music.Play();
    }
}
