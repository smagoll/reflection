using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI recordText;

    public void ButtonPlay()
    {
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        recordText.text = DataManager.instance.HighScore.ToString();
    }
}
