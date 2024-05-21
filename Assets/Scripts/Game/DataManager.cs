using UnityEngine;
using YG;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public LevelsAsset levelsAsset;

    public int HighScore { get; set; }
    public int Level { get; set; }
    public int SelectedLevel { get; set; }
    
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
        YandexGame.savesData.level = Level;
        
        YandexGame.SaveProgress();
    }
    
    private void GetData()
    {
        Level = YandexGame.savesData.level;
    }

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
}
