using System;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    public static AudioUI instance;
    
    [SerializeField]
    private AudioSource ui;

    [SerializeField]
    private AudioClip buttonClick;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void PlaySFX(AudioClip audioClip)
    {
        ui.PlayOneShot(audioClip);
    }

    public void ButtonClick() => PlaySFX(buttonClick);
}
