using DG.Tweening;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotation;
    
    private void Start()
    {
        transform.DORotate(rotation, 2f).SetLoops(-1).SetRelative().SetEase(Ease.Linear);
    }
}
