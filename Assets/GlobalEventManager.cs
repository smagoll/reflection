using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent GameOver = new();
    public static UnityEvent RestartGame = new();
    
    public static UnityEvent UpdateTextScore = new();
}
