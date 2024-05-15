using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private int countBalls;

    public int Id => id;
    public int CountBalls => countBalls;
    public Ring[] Rings { get; private set; }

    private void Awake()
    {
        Rings = GetComponentsInChildren<Ring>();
    }

    public void Unlock()
    {
        if (id == DataManager.instance.Level)
        {
            DataManager.instance.Level++;
            DataManager.instance.Save();
        }
    }
}
