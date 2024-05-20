using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButtonDefault : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent endClick = new();
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Pressed);
    }

    private void Pressed()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(.8f, .3f))
            .Append(transform.DOScale(1f, .1f))
            .AppendCallback(() =>
            {
                endClick.Invoke();
                AudioController.instance.PlaySFX(AudioController.instance.buttonClick);
            })
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioController.instance.PlaySFX(AudioController.instance.buttonEnter);
    }
}