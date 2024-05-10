using System;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnpointProjectile;
    [SerializeField]
    private Ball prefabBall;

    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private PlayerController playerController;

    private int score = 0;

    public float Score => score;
    public float HighScore => DataManager.instance.HighScore;

    private void Start()
    {
        SpawnProjectile();
    }

    public void SpawnProjectile()
    {
        var ball = Instantiate(prefabBall, spawnpointProjectile.position, Quaternion.identity);
        playerController.SetBall(ball);
    }
    
    private void IncreaseScore()
    {
        score++;
        uiManager.UpdateTextScore(score);
    }

    private void SaveLeaderBoard()
    {
        if (score > HighScore)
        {
            YandexGame.NewLeaderboardScores("Leaders", score);
            DataManager.instance.HighScore = score;
            DataManager.instance.Save();
        }
    }
    
    private void OnEnable()
    {
        GlobalEventManager.UpdateTextScore.AddListener(IncreaseScore);
        GlobalEventManager.GameOver.AddListener(SaveLeaderBoard);
        GlobalEventManager.RestartGame.AddListener(SpawnProjectile);
        GlobalEventManager.RestartGame.AddListener(YandexGame.FullscreenShow);
        GlobalEventManager.RestartGame.AddListener(() => score = 0);
    }
    
    private void OnDisable()
    {
        GlobalEventManager.UpdateTextScore.RemoveListener(IncreaseScore);
        GlobalEventManager.GameOver.RemoveListener(SaveLeaderBoard);
        GlobalEventManager.RestartGame.RemoveListener(SpawnProjectile);
        GlobalEventManager.RestartGame.RemoveListener(YandexGame.FullscreenShow);
        GlobalEventManager.RestartGame.RemoveListener(() => score = 0);
    }
}
