using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Zenject.SpaceFighter;

public class BallsSpawner : MonoBehaviour
{
    private PlayerController playerController;
    private UIManager uiManager;
    
    [SerializeField]
    private Transform spawnpointProjectile;
    [SerializeField]
    private Transform transformBalls;
    [SerializeField]
    private Ball prefabBall;
    private ObjectPool<Ball> poolBalls;

    private int countBalls;
    private CancellationTokenSource cancellationTokenSource;
    
    public int CountBalls
    {
        get => countBalls;
        set
        {
            countBalls = value;
            uiManager.UpdateTextCountBalls(countBalls);
        }
    }

    
    [Inject]
    private void Construct(PlayerController playerController, UIManager uiManager)
    {
        this.playerController = playerController;
        this.uiManager = uiManager;
    }

    private void Awake()
    {
        poolBalls = CreatePool(prefabBall);
    }

    private async void SpawnProjectile()
    {
        if (countBalls > 0)
        {
            await UniTask.WaitForSeconds(.1f);
            var ball = poolBalls.Get();
            playerController.SetBall(ball);
            ball.Init(poolBalls, spawnpointProjectile, cancellationTokenSource: cancellationTokenSource);
            if (countBalls == 1) ball.isLast = true;
        }
        else
        {
            Debug.Log("balls empty");
        }
    }

    private void OnEnable()
    {
        GlobalEventManager.SpawnBall.AddListener(SpawnProjectile);
        GlobalEventManager.DecreaseCountBalls.AddListener(() => CountBalls--);
        GlobalEventManager.LevelLoaded.AddListener(SpawnProjectile);
        GlobalEventManager.LoseLevel.AddListener(ClearBalls);
        GlobalEventManager.CompleteLevel.AddListener(ClearBalls);
        GlobalEventManager.StartLevel.AddListener(() => cancellationTokenSource = new CancellationTokenSource());
    }
    private void OnDisable()
    {
        GlobalEventManager.SpawnBall.RemoveListener(SpawnProjectile);
        GlobalEventManager.DecreaseCountBalls.RemoveListener(() => CountBalls--);
        GlobalEventManager.LevelLoaded.RemoveListener(SpawnProjectile);
        GlobalEventManager.LoseLevel.RemoveListener(ClearBalls);
        GlobalEventManager.CompleteLevel.RemoveListener(ClearBalls);
        GlobalEventManager.StartLevel.RemoveListener(() => cancellationTokenSource = new CancellationTokenSource());
    }

    private void ClearBalls()
    {
        poolBalls.Clear();
        foreach (Transform ball in transformBalls) Destroy(ball.gameObject);
        cancellationTokenSource.Cancel();
    }
    
    private ObjectPool<Ball> CreatePool(Ball ball)
    {
        ObjectPool<Ball> pool = new(() =>
        {
            return Instantiate(ball, transformBalls);
        }, ball => {
            ball.gameObject.SetActive(true);
        }, ball => {
            ball.gameObject.SetActive(false);
        }, ball => {
            Destroy(ball);
        }, false);

        return pool;
    }
}
