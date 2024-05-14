using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class BallsSpawner : MonoBehaviour
{
    private PlayerController playerController;
    private UIManager uiManager;
    
    [SerializeField]
    private Transform spawnpointProjectile;
    [SerializeField]
    private Ball prefabBall;

    private int countBalls;
    
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
        CountBalls = 1;
    }

    private void Start()
    {
        SpawnProjectile();
    }

    private async void SpawnProjectile()
    {
        if (countBalls > 0)
        {
            await UniTask.WaitForSeconds(.1f);
            var ball = Instantiate(prefabBall, spawnpointProjectile.position, Quaternion.identity);
            playerController.SetBall(ball);
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
    }
    private void OnDisable()
    {
        GlobalEventManager.SpawnBall.RemoveListener(SpawnProjectile);
        GlobalEventManager.DecreaseCountBalls.RemoveListener(() => CountBalls--);
    }
}
