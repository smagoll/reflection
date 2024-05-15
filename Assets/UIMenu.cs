using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject levelsList;
    [SerializeField]
    private GameObject buttonPlay;

    public void ButtonPlay()
    {
        //SceneManager.LoadScene("Game");
        buttonPlay.SetActive(false);
        levelsList.SetActive(true);
    }
}
