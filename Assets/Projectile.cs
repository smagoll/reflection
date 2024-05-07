using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float forceAttraction;
    [SerializeField]
    private float speed = 5f;

    private float calculateSpeed;
    
    private void FixedUpdate()
    {
        if (target == null) return;
        
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    
    public void SwitchDirection(Transform target)
    {
        this.target = target;
        UpdateSpeed();
        
        var dirToPoint = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dirToPoint);
    }

    private void UpdateSpeed()
    {
        speed++;
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
