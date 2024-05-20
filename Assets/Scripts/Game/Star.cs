using DG.Tweening;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, 360f, 0f), 2f).SetLoops(-1).SetRelative().SetEase(Ease.Linear);
    }
}
