using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelMaster : MonoBehaviour
{
    [SerializeField]
    private Transform map;
    [SerializeField]
    private BallsSpawner ballsSpawner;
    [SerializeField]
    private LevelLoader levelLoader;

    private Level currentLevel;
    private AssetReferenceGameObject currentLevelReference;

    [SerializeField]
    private LevelsAsset levelsAsset;
    
    public void CreateLevel(int numberLevel)
    {
        var levelReference = GetLevel(numberLevel);
        currentLevelReference = levelReference;
        var handle = levelReference.InstantiateAsync(map);
        levelLoader.LoadLevel(handle);
        handle.Completed += OnLevelInstantiated;
    }

    private void OnLevelInstantiated(AsyncOperationHandle<GameObject> handle) // Уровень загружен
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if(currentLevel != null) currentLevelReference.ReleaseInstance(currentLevel.gameObject);
            Init(handle.Result.GetComponent<Level>());
            GlobalEventManager.LevelLoaded?.Invoke();
        }
    }

    private void Init(Level level)
    {
        currentLevel = level;
        ballsSpawner.CountBalls = level.CountBalls;
    }
    
    private AssetReferenceGameObject GetLevel(int numberLevel)
    {
        return levelsAsset.LevelInfos.FirstOrDefault(x => x.id == numberLevel)?.assetReference;
    }

    private void CheckLevelComplete()
    {
        if (currentLevel.Rings.Count(x => x.IsDone) == currentLevel.Rings.Length)
        {
            GlobalEventManager.CompleteLevel?.Invoke();
        }
    }

    private void UnlockLevel() => currentLevel.Unlock();

    private void OnEnable()
    {
        GlobalEventManager.CheckLevelComplete.AddListener(CheckLevelComplete);
        GlobalEventManager.CompleteLevel.AddListener(UnlockLevel);
    }
    
    private void OnDisable()
    {
        GlobalEventManager.CheckLevelComplete.RemoveListener(CheckLevelComplete);
        GlobalEventManager.CompleteLevel.RemoveListener(UnlockLevel);
    }
}
