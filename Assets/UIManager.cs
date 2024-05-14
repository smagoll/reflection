using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textCountBalls;
    [SerializeField]
    private GameObject windowGameOver;

    [SerializeField]
    private GameManager gameManager;
    
    private void ShowWindowGameOver()
    {
        windowGameOver.SetActive(true);
    }
    private void HideWindowGameOver()
    {
        windowGameOver.SetActive(false);
    }

    private void HideUnusualUI()
    {
        textCountBalls.gameObject.SetActive(false);
        textCountBalls.text = "0";
    }
    
    private void ShowUnusualUI()
    {
        textCountBalls.gameObject.SetActive(true);
    }
    
    public void UpdateTextCountBalls(int countBalls)
    {
        textCountBalls.text = countBalls.ToString();
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
