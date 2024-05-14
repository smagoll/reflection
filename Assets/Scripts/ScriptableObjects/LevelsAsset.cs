using UnityEngine;

[CreateAssetMenu(fileName = "LevelsAsset", menuName = "Levels")]
public class LevelsAsset : ScriptableObject
{
    public LevelInfo[] LevelInfos;
}
