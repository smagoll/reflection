using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    private bool isFlight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (playerTransform != null) transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, 0.5f);
        if(isFlight) rb.AddForce(new Vector3(0,-50,0));
    }

    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void Throw(Vector3 direction)
    {
        playerTransform = null;
        rb.AddRelativeForce(new Vector3(direction.x, direction.y + 1.7f, 1) * Time.fixedDeltaTime * speed, ForceMode.Impulse);
        rb.useGravity = true;
        isFlight = true;
        
        DestroyBall(5f).Forget();
    }

    private async UniTask DestroyBall(float timeBeforeDestroy)
    {
        await UniTask.WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
