using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent LoseLevel = new();
    public static UnityEvent CompleteLevel = new();
    
    public static UnityEvent StartLevel = new();
    public static UnityEvent LevelLoaded = new();
    
    public static UnityEvent DecreaseCountBalls = new();
    public static UnityEvent SpawnBall = new();
    public static UnityEvent CheckLevelComplete = new();
    public static UnityEvent BuyNewBalls = new();
}
