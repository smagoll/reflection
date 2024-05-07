using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    [SerializeField]
    private Transform pointPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            projectile.SwitchDirection(pointPlayer);
        }
    }
}
