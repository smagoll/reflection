using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField]
    private GameObject vfxHit;
    
    private void OnCollisionEnter(Collision other)
    {
        AudioController.instance.PlaySFX(AudioController.instance._glassHitThumb);
        Instantiate(vfxHit, other.transform.position, Quaternion.identity);
    }
}
