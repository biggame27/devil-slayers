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
    [SerializeField]
    private Canvas deathCanvas;
    public void Start()
    {
        pauseCanvas.enabled = false;
        deathCanvas.enabled =false;
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
        canvas.enabled = false;
        pauseCanvas.enabled = true;
        Debug.Log(canvas.enabled);
        Time.timeScale = 0;
        paused = true;
    }

    public void Death()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        deathCanvas.GetComponent<Canvas>().enabled = true;
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

    public void Stop()
    {
        Time.timeScale = 0;
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void Begin()
    {
        Time.timeScale = 1;
        canvas.GetComponent<Canvas>().enabled = true;
    }
}
