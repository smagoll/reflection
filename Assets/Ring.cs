using System;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private bool isDone;
    public bool IsDone => isDone;

    [SerializeField]
    private Material ringCompleted;
    [SerializeField]
    private MeshRenderer ring;
    [SerializeField]
    private GameObject hitRing;
    
    public void Hit()
    {
        if (!isDone)
        {
            isDone = true;
            Debug.Log("hit");
            ring.material = ringCompleted;
            GlobalEventManager.CheckLevelComplete?.Invoke();
        }
        AudioController.instance.PlaySFX(AudioController.instance._hit);
        Instantiate(hitRing, transform.position, Quaternion.identity);
    }
}
