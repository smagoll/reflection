using UnityEngine;

public class SoundCollision : MonoBehaviour
{
    [SerializeField]
    private AudioClip sfx;

    private void OnCollisionEnter(Collision other)
    {
        AudioController.instance.PlaySFXSmall(sfx);
    }
}
