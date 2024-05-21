using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MusicVisualizer : MonoBehaviour
{
    public static MusicVisualizer instance;

    public float[] spectrumWidth;

    [SerializeField]
    private AudioSource music;
    
    private void Awake()
    {
        if (instance == null) instance = this;

        music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioController>().Music;
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
