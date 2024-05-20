using UnityEngine;

public class TopColliderRing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            ball.EnterRing = transform.parent.gameObject;
        }
    }
}
