using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelList : MonoBehaviour
{
    [SerializeField]
    private Transform listTransform;
    [SerializeField]
    private LevelsAsset levelsAsset;
    [SerializeField]
    private LevelUI prefabLevelUI;

    private void OnEnable()
    {
        foreach (var levelInfo in levelsAsset.LevelInfos)
        {
            var levelUI = Instantiate(prefabLevelUI, listTransform);
            levelUI.Init(levelInfo.id);
        }
    }
}
