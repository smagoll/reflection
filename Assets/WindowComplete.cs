using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowComplete : MonoBehaviour
{
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private LevelsAsset levelsAsset;

    private void OnEnable()
    {
        if (DataManager.instance.SelectedLevel < levelsAsset.LevelInfos.Length)
            nextButton.SetActive(true);
        else
            nextButton.SetActive(false);
    }
}
