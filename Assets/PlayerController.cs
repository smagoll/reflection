using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CinemachineShake cinemachineShake;
    [SerializeField]
    private Ball ball;
    
    private float xMove = 0;
    private float yMove = 0;
    [SerializeField] private float sensivity;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Debug.Log("click");

            ball.Throw();
        }
    }

    public void SetBall(Ball ball)
    {
        this.ball = ball;
        ball.Init(transform);
    }
}
