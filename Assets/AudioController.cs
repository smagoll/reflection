using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sfx;
    [SerializeField]
    private AudioSource sfxSmall;
    
    public AudioClip _throw;
    public AudioClip _rebound;
    public AudioClip _hit;
    public AudioClip _glassHitThumb;
    public AudioClip _levelComplete;
    public AudioClip _levelLose;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfx.PlayOneShot(audioClip);
    }
    
    public void PlaySFXSmall(AudioClip audioClip)
    {
        sfxSmall.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        music.clip = audioClip;
        music.Play();
    }

    private void OnEnable()
    {
        GlobalEventManager.CompleteLevel.AddListener(() => PlaySFXSmall(_levelComplete));
        GlobalEventManager.LoseLevel.AddListener(() => PlaySFXSmall(_levelLose));
    }
    
    private void OnDisable()
    {
        GlobalEventManager.CompleteLevel.AddListener(() => PlaySFXSmall(_levelComplete));
        GlobalEventManager.LoseLevel.AddListener(() => PlaySFXSmall(_levelLose));
    }
}
