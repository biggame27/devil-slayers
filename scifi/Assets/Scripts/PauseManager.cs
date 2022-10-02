using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool paused = false;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Canvas pauseCanvas;
    public void Start()
    {
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void OnClick()
    {
        if(!paused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        pauseCanvas.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame()
    {
        canvas.GetComponent<Canvas>().enabled = true;
        pauseCanvas.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        paused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
