using System;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BallsSpawner ballsSpawner;
    [SerializeField]
    private LevelMaster levelMaster;

    public BallsSpawner BallsSpawner => ballsSpawner;

    private void Start()
    {
        levelMaster.CreateLevel(1);
    }

    private void OnEnable()
    {
        GlobalEventManager.RestartGame.AddListener(YandexGame.FullscreenShow);
    }
    
    private void OnDisable()
    {
        GlobalEventManager.RestartGame.RemoveListener(YandexGame.FullscreenShow);
    }
}
