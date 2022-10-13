using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highestScore;

    public void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
            highestScore.text = "Highest score: " + data.score;
        else
            highestScore.text = "Highest score: 0";
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
