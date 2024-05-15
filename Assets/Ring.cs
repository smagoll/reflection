using UnityEngine;

public class Ring : MonoBehaviour
{
    private bool isDone;
    public bool IsDone => isDone;
    
    public void Hit()
    {
        if (!isDone)
        {
            isDone = true;
            Debug.Log("hit");
            GlobalEventManager.CheckLevelComplete?.Invoke();
            AudioController.instance.PlaySFX(AudioController.instance._hit);
        }
    }
}
