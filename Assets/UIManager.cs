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
    private GameObject windowLose;
    [SerializeField]
    private GameObject windowComplete;

    [SerializeField]
    private GameManager gameManager;
    
    private void ShowWindowGameOver()
    {
        windowLose.SetActive(true);
    }
    private void HideWindowGameOver()
    {
        windowLose.SetActive(false);
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
        ShowUnusualUI();
        HideWindowGameOver();
        GlobalEventManager.StartLevel?.Invoke();
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextButton()
    {
        ShowUnusualUI();
        windowComplete.SetActive(false);
        DataManager.instance.SelectedLevel++;
        GlobalEventManager.StartLevel?.Invoke();
    }

    public void AdButton()
    {
        
    }

    private void OnEnable()
    {
        GlobalEventManager.LoseLevel.AddListener(ShowWindowGameOver);
        GlobalEventManager.LoseLevel.AddListener(HideUnusualUI);
        GlobalEventManager.CompleteLevel.AddListener(() => windowComplete.SetActive(true));
    }
    
    private void OnDisable()
    {
        GlobalEventManager.LoseLevel.RemoveListener(ShowWindowGameOver);
        GlobalEventManager.LoseLevel.RemoveListener(HideUnusualUI);
        GlobalEventManager.CompleteLevel.RemoveListener(() => windowComplete.SetActive(true));
    }
}
