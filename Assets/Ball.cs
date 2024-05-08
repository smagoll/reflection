using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (playerTransform != null) transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, 0.5f);
        
    }

    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void Throw()
    {
        playerTransform = null;
        rb.AddRelativeForce(Vector3.forward * Time.fixedDeltaTime * speed, ForceMode.Impulse);
        rb.useGravity = true;
    }
}
