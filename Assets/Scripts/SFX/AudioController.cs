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
    
    [Header("Game")]
    public AudioClip _throw;
    public AudioClip _rebound;
    public AudioClip _hit;
    public AudioClip _glassHitThumb;
    public AudioClip _levelComplete;
    public AudioClip _levelLose;

    [Header("UI")] 
    public AudioClip buttonClick;
    public AudioClip buttonEnter;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
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
