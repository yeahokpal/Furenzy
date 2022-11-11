using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public bool isPaused = false;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        GameObject.Find("PauseMenu").SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GameObject.Find("PauseMenu").SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        // Change the scene to the main menu
    }
}
