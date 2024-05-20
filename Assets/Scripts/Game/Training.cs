using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    [SerializeField]
    private Image tapFinger;

    private void Start()
    {
        if (DataManager.instance.Level == 1)
        {
            LaunchTrainingUI();
        }
        else
        {
            HideTraining();
        }
    }

    private void LaunchTrainingUI()
    {
        DOTween.Sequence()
            .Append(tapFinger.DOFade(1f, .7f))
            .Append(tapFinger.transform.DOMoveY(Screen.height / 3f, 1.5f))
            .Append(tapFinger.DOFade(0f, .7f))
            .AppendInterval(1f).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void HideTraining()
    {
        Destroy(gameObject);
    }
    
    private void OnEnable()
    {
        GlobalEventManager.DecreaseCountBalls.AddListener(HideTraining);
    }

    private void OnDisable()
    {
        GlobalEventManager.DecreaseCountBalls.RemoveListener(HideTraining);
    }
}
