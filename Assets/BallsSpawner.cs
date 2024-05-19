using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

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
        GlobalEventManager.StartLevel.AddListener(ClearBalls);
        GlobalEventManager.BuyNewBalls.AddListener(BuyAdditionalBalls);
        GlobalEventManager.CompleteLevel.AddListener(() => cancellationTokenSource?.Cancel());
        GlobalEventManager.LoseLevel.AddListener(() => cancellationTokenSource?.Cancel());
    }
    private void OnDisable()
    {
        GlobalEventManager.SpawnBall.RemoveListener(SpawnProjectile);
        GlobalEventManager.DecreaseCountBalls.RemoveListener(() => CountBalls--);
        GlobalEventManager.LevelLoaded.RemoveListener(SpawnProjectile);
        GlobalEventManager.StartLevel.RemoveListener(ClearBalls);
        GlobalEventManager.BuyNewBalls.RemoveListener(BuyAdditionalBalls);
        GlobalEventManager.CompleteLevel.RemoveListener(() => cancellationTokenSource?.Cancel());
        GlobalEventManager.LoseLevel.RemoveListener(() => cancellationTokenSource?.Cancel());
    }

    private void ClearBalls()
    {
        //cancellationTokenSource?.Cancel();
        //cancellationTokenSource?.Dispose();
        
        var balls = transformBalls.GetComponentsInChildren<Ball>();
        foreach (var ball in balls) poolBalls.Release(ball);

        cancellationTokenSource = new CancellationTokenSource();
    }
    
    private ObjectPool<Ball> CreatePool(Ball ball)
    {
        ObjectPool<Ball> pool = new(() =>
        {
            return Instantiate(ball, transformBalls);
        }, ball => {
            ball.gameObject.SetActive(true);
        }, ball => {
            ball.ResetStats();
        }, ball => {
            Destroy(ball);
        }, false);

        return pool;
    }

    private void BuyAdditionalBalls()
    {
        ClearBalls();
        CountBalls = 3;
        SpawnProjectile();
    }
}
