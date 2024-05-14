using System;
using UnityEngine;
using YG;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int HighScore { get; set; }
    public int Level { get; set; }
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        GetData();
    }

    public void Save()
    {
        YandexGame.savesData.highScore = HighScore;
        YandexGame.savesData.level = Level;
        
        YandexGame.SaveProgress();
    }
    
    private void GetData()
    {
        HighScore = YandexGame.savesData.highScore;
        Level = YandexGame.savesData.level;
    }

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
}
