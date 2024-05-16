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

    public int testLevel;
    
    public BallsSpawner BallsSpawner => ballsSpawner;

    private void Start()
    {
        GlobalEventManager.StartLevel?.Invoke();
    }

    public void LoadLevel()
    {
        //levelMaster.CreateLevel(DataManager.instance.SelectedLevel);
        levelMaster.CreateLevel(testLevel);
    }
    
    private void OnEnable()
    {
        GlobalEventManager.LoseLevel.AddListener(YandexGame.FullscreenShow);
        GlobalEventManager.CompleteLevel.AddListener(YandexGame.FullscreenShow);
        GlobalEventManager.StartLevel.AddListener(LoadLevel);
    }
    
    private void OnDisable()
    {
        GlobalEventManager.LoseLevel.RemoveListener(YandexGame.FullscreenShow);
        GlobalEventManager.CompleteLevel.RemoveListener(YandexGame.FullscreenShow);
        GlobalEventManager.StartLevel.RemoveListener(LoadLevel);
    }
}
