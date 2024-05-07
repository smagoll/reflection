using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            projectile.Death();
            GlobalEventManager.GameOver?.Invoke();
            Debug.Log("game over");
        }
    }
}
