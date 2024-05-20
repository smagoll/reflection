using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance;

    private AsyncOperation loadingAsyncOperation;

    [SerializeField]
    private Image background;
    [SerializeField]
    private bool hideOnStart;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        if (hideOnStart) HideSceneTransition();
    }

    public static void LoadScene(string sceneName)
    {
        instance.ShowSceneTransition();
        instance.loadingAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
    }

    public void ShowSceneTransition()
    {
        background.DOFade(255f, 2f).SetUpdate(true);
    }
    
    public void HideSceneTransition()
    {
        background.DOFade(0f, 2f).SetUpdate(true);
    }

    private IEnumerator Wait(float time)
    {
        instance.loadingAsyncOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(time);
        instance.loadingAsyncOperation.allowSceneActivation = true;
    }
}
