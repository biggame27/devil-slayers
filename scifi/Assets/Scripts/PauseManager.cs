using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public bool paused = false;
    public void OnClick()
    {
        if(!paused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
    }
}
