using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicVisualizer : MonoBehaviour
{
    public static MusicVisualizer instance;

    public float[] spectrumWidth;

    [SerializeField]
    private AudioSource music;
    
    private void Awake()
    {
        if (instance == null) instance = this;

        spectrumWidth = new float[64];
    }

    private void FixedUpdate()
    {
        music.GetSpectrumData(spectrumWidth, 0, FFTWindow.Blackman);
    }

    public float GetFrequencyRange(int start, int end, int mult)
    {
        return spectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }
}
