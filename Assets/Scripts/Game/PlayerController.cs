using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CinemachineShake cinemachineShake;
    [SerializeField]
    private Ball ball;

    private GameManager gameManager;
    
    [Header("Throw Options")]
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float minForce;
    [SerializeField]
    private float minSpeedMouse = 3f;
    [SerializeField]
    private float maxSpeedMouse = 10f;
    
    private bool isTouch;
    private Vector3 startPosition;
    private Vector2 lastMousePosition;
    private Queue<Vector2> lastMousePositions;
    private float multiply;

    private float RangeOffsetY => maxForce - minForce;
    private float RangeSpeedMouse => maxSpeedMouse - minSpeedMouse;
    private float MaxDistance => Screen.height / 3;
    
    [Inject]
    private void Construct(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        lastMousePosition = Input.mousePosition;
        lastMousePositions = new Queue<Vector2>(10);
        multiply = RangeOffsetY / RangeSpeedMouse;
    }

    private void Update()
    {
        if (ball == null || gameManager.isPause) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            isTouch = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if(isTouch) ResetDrag();
        }

        if (isTouch)
        {
            Drag();
        }
    }

    public void SetBall(Ball ball)
    {
        this.ball = ball;
    }

    private void Drag()
    {
        CalculateSpeedMouse();
        var currentMousePosition = Input.mousePosition;
        var heading = currentMousePosition - startPosition;
        var distance = heading.magnitude;
        
        if (distance > MaxDistance)
        {
            if (currentMousePosition.y - startPosition.y > 0)
            {
                var direction = heading / distance;
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
        var speedMouse = lastMousePositions.Average(x => x.magnitude);
        var force = GetForce(speedMouse);
        
        ball.Throw(direction, force);
        ball = null;
        
        GlobalEventManager.SpawnBall?.Invoke();
        AudioController.instance.PlaySFX(AudioController.instance._throw);
    }

    private void CalculateSpeedMouse()
    {
        var AxisX = ((Input.mousePosition.x - lastMousePosition.x) / Time.deltaTime) / Screen.width;
        var AxisY = ((Input.mousePosition.y - lastMousePosition.y) / Time.deltaTime) / Screen.height;
        if (lastMousePositions.Count >= 10) lastMousePositions.Dequeue();
        lastMousePositions.Enqueue(new Vector2(AxisX, AxisY));
        
        lastMousePosition = Input.mousePosition;
    }

    private float GetForce(float speedMouse)
    {
        if (speedMouse < maxSpeedMouse)
        {
            return minForce + speedMouse * multiply;
        }
        else
            return maxForce;
    }
}
