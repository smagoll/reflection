using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CinemachineShake cinemachineShake;
    [SerializeField]
    private Ball ball;

    private GameManager gameManager;
    
    [FormerlySerializedAs("distance")] [SerializeField]
    private float maxDistance = 500f;
    private bool isTouch;
    private Vector3 startPosition;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    
    private void Update()
    {
        if (ball == null) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click_down");

            startPosition = Input.mousePosition;
            isTouch = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("click_up");

            ResetDrag();
        }

        if (isTouch)
        {
            Drag();
        }
    }

    public void SetBall(Ball ball)
    {
        this.ball = ball;
        ball.Init(transform);
    }

    private void Drag()
    {
        var currentMousePosition = Input.mousePosition;
        var heading = currentMousePosition - startPosition;
        var distance = heading.magnitude;
        if (distance > maxDistance)
        {
            if (currentMousePosition.y - startPosition.y > 0)
            {
                var direction = heading / distance;
                Debug.Log("throw");
                Debug.Log(direction);
                ThrowBall(direction);
                ResetDrag();
            }
        }
    }

    private void ResetDrag()
    {
        startPosition = Vector3.zero;
        isTouch = false;
    }

    private void ThrowBall(Vector3 direction)
    {
        ball.Throw(direction);
        ball = null;
        gameManager.SpawnProjectile();
    }
}
