using UnityEngine;

public class BottomColliderRing : MonoBehaviour
{
    [SerializeField]
    private Ring ring;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            if (ball.EnterRing == transform.parent.gameObject)
            {
                ring.Hit();
            }
        }
    }
}
