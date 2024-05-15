using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToDie = 3f;
    
    private bool isFlight;
    public bool isLast;
    private ObjectPool<Ball> pool;
    private CancellationTokenSource cancellationTokenSource;
    
    public bool IsEnterRing { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isFlight) rb.AddForce(new Vector3(0,-50,0));
    }

    public void Init(ObjectPool<Ball> pool, Transform spawnpoint, CancellationTokenSource cancellationTokenSource)
    {
        this.pool = pool;
        transform.position = spawnpoint.position;
        this.cancellationTokenSource = cancellationTokenSource;
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.AddRelativeForce(new Vector3(direction.x * 2, direction.y + 1.7f, 1) * Time.fixedDeltaTime * force, ForceMode.Impulse);
        rb.useGravity = true;
        isFlight = true;
        
        GlobalEventManager.DecreaseCountBalls?.Invoke();
        DestroyBallDelay(timeToDie, cancellationTokenSource.Token).Forget();
    }

    private async UniTask DestroyBallDelay(float timeBeforeDestroy, CancellationToken cancellationToken)
    {
        await UniTask.WaitForSeconds(timeBeforeDestroy, cancellationToken: cancellationToken);
        if(cancellationToken.IsCancellationRequested) return;
        DestroyBall();
    }

    private void DestroyBall()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isFlight = false;
        pool.Release(this);
        if(isLast) GlobalEventManager.LoseLevel?.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        AudioController.instance.PlaySFX(AudioController.instance._rebound);
    }
}
