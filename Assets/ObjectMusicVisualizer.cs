using UnityEngine;

public class ObjectMusicVisualizer : MonoBehaviour
{
    enum TypeMusicVisualizer
    {
        Bass,
        NearBass,
        Middle,
        High
    }

    [SerializeField]
    private TypeMusicVisualizer typeMusicVisualizer;
    [SerializeField]
    private float force = .1f;

    [SerializeField]
    private float bufferChange = 0.005f;
    [SerializeField]
    private float bufferChangeMultiply = 1.2f;

    private float currentBuffer;
    private float bufferDecrease;
    
    private void FixedUpdate()
    {
        Shake();
    }

    private void Shake()
    {
        int start = 0, end = 0, mult = 0;
        
        switch (typeMusicVisualizer)
        {
            case TypeMusicVisualizer.Bass:
                start = 0;
                end = 7;
                mult = 10;
                break;
            case TypeMusicVisualizer.NearBass:
                start = 7;
                end = 15;
                mult = 100;
                break;
            case TypeMusicVisualizer.Middle:
                start = 15;
                end = 30;
                mult = 200;
                break;
            case TypeMusicVisualizer.High:
                start = 30;
                end = 32;
                mult = 1000;
                break;
        }

        var frequencyRange = MusicVisualizer.instance.GetFrequencyRange(start, end, mult);

        CalculateBuffer(frequencyRange);
        
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentBuffer, currentBuffer, currentBuffer), force);
    }

    private void CalculateBuffer(float frequency)
    {
        if (frequency > currentBuffer)
        {
            currentBuffer = frequency;
            bufferDecrease = bufferChange;
        }
        
        if (frequency < currentBuffer)
        {
            currentBuffer = frequency;
            currentBuffer -= bufferDecrease;
            bufferDecrease *= bufferChangeMultiply;
        }
    }
}
