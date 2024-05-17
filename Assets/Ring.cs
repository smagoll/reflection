using UnityEngine;

public class Ring : MonoBehaviour
{
    private bool isDone;
    public bool IsDone => isDone;

    [SerializeField]
    private Material ringCompleted;
    
    public void Hit()
    {
        if (!isDone)
        {
            isDone = true;
            Debug.Log("hit");
            gameObject.GetComponent<MeshRenderer>().material = ringCompleted;
            GlobalEventManager.CheckLevelComplete?.Invoke();
            AudioController.instance.PlaySFX(AudioController.instance._hit);
        }
    }
}
