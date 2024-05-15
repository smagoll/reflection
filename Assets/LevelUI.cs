using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNumber;

    private int id;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Init(int numberLevel)
    {
        id = numberLevel;
        textNumber.text = id.ToString();
        if (numberLevel > DataManager.instance.Level) button.interactable = false;
    }

    public void LaunchLevel()
    {
        DataManager.instance.SelectedLevel = id;
        SceneManager.LoadScene("Game");
    }
}
