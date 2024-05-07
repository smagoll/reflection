using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform pointBatting;
    [SerializeField]
    private Transform pointEnemy;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Debug.Log("click");

            RaycastHit[] hits = Physics.SphereCastAll(pointBatting.position, 2f, Vector3.forward);
            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent(out Projectile projectile))
                {
                    projectile.SwitchDirection(pointEnemy);
                    GlobalEventManager.UpdateTextScore?.Invoke();
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointBatting.position, 2f);
    }
}
