using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private GameObject windowGameOver;
    [SerializeField]
    private TextMeshProUGUI textFinalScore;

    [SerializeField]
    private GameManager gameManager;
    
    private void ShowWindowGameOver()
    {
        windowGameOver.SetActive(true);
        textFinalScore.text = gameManager.Score.ToString();
    }
    private void HideWindowGameOver()
    {
        windowGameOver.SetActive(false);
        textFinalScore.text = "0";
    }

    private void HideUnusualUI()
    {
        textScore.gameObject.SetActive(false);
        textScore.text = "0";
    }
    
    private void ShowUnusualUI()
    {
        textScore.gameObject.SetActive(true);
    }
    
    public void UpdateTextScore(float score)
    {
        textScore.text = score.ToString();
    }

    public void RestartButton()
    {
         GlobalEventManager.RestartGame?.Invoke();
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
    
    private void OnEnable()
    {
        GlobalEventManager.GameOver.AddListener(ShowWindowGameOver);
        GlobalEventManager.GameOver.AddListener(HideUnusualUI);
        GlobalEventManager.RestartGame.AddListener(ShowUnusualUI);
        GlobalEventManager.RestartGame.AddListener(HideWindowGameOver);
    }
    
    private void OnDisable()
    {
        GlobalEventManager.GameOver.RemoveListener(ShowWindowGameOver);
        GlobalEventManager.GameOver.RemoveListener(HideUnusualUI);
        GlobalEventManager.RestartGame.RemoveListener(ShowUnusualUI);
        GlobalEventManager.RestartGame.RemoveListener(HideWindowGameOver);
    }
}
