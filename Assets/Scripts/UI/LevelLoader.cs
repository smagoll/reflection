using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        HideLevelLoader();
    }

    public void LoadLevel(AsyncOperationHandle<GameObject> handle)
    {
        ShowLevelLoader();
        StartCoroutine(Load(handle));
    }
    
    private IEnumerator Load(AsyncOperationHandle<GameObject> handle)
    {
        while (handle.PercentComplete < 1 && handle.Status != AsyncOperationStatus.Succeeded)
        {
            yield return null;
        }
        HideLevelLoader();
    }
    
    public void ShowLevelLoader()
    {
        image.DOFade(255f, 1f).SetUpdate(true);
    }
    
    public void HideLevelLoader()
    {
        image.DOFade(0f, 1f).SetUpdate(true);
    }
}
