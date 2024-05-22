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
    public bool isPause;
    
    public BallsSpawner BallsSpawner => ballsSpawner;

    private void Start()
    {
        GlobalEventManager.StartLevel?.Invoke();
    }

    public void LoadLevel()
    {
        levelMaster.CreateLevel(DataManager.instance.SelectedLevel);
        //levelMaster.CreateLevel(testLevel);
        isPause = false;
        YandexGame.FullscreenShow();
    }

    private void Pause()
    {
        isPause = true;
    }
    
    private void OnEnable()
    {
        GlobalEventManager.LoseLevel.AddListener(Pause);
        GlobalEventManager.CompleteLevel.AddListener(Pause);
        GlobalEventManager.StartLevel.AddListener(LoadLevel);
        GlobalEventManager.BuyNewBalls.AddListener(() => isPause = false);
    }
    
    private void OnDisable()
    {
        GlobalEventManager.LoseLevel.RemoveListener(Pause);
        GlobalEventManager.CompleteLevel.RemoveListener(Pause);
        GlobalEventManager.StartLevel.RemoveListener(LoadLevel);
        GlobalEventManager.BuyNewBalls.RemoveListener(() => isPause = false);
    }
}
