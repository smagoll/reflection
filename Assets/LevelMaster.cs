using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelMaster : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    private Level currentLevel;
    private AssetReferenceGameObject currentLevelReference;

    [SerializeField]
    private LevelsAsset levelsAsset;
    
    public void CreateLevel(int numberLevel)
    {
        var levelReference = GetLevel(numberLevel);
        currentLevelReference = levelReference;
        var handle = levelReference.InstantiateAsync(map);
        handle.Completed += OnLevelInstantiated;
    }

    private void OnLevelInstantiated(AsyncOperationHandle<GameObject> handle) // Уровень загружен
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if(currentLevel != null) currentLevelReference.ReleaseInstance(currentLevel.gameObject);
            currentLevel = handle.Result.GetComponent<Level>();
        }
    }

    private AssetReferenceGameObject GetLevel(int numberLevel)
    {
        return levelsAsset.LevelInfos.FirstOrDefault(x => x.id == numberLevel)?.assetReference;
    }
}
