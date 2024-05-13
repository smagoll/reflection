using Cysharp.Threading.Tasks;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float timeToDie = 3f;
    
    private bool isFlight;
    
    public bool IsEnterRing { get; set; }

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

    public void Throw(Vector3 direction, float force)
    {
        playerTransform = null;
        rb.AddRelativeForce(new Vector3(direction.x * 2, direction.y + 1.7f, 1) * Time.fixedDeltaTime * force, ForceMode.Impulse);
        rb.useGravity = true;
        isFlight = true;
        
        DestroyBall(timeToDie).Forget();
    }

    private async UniTask DestroyBall(float timeBeforeDestroy)
    {
        await UniTask.WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
