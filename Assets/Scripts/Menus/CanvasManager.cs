using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;

    private void Start()
    {
        Resume();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        // Change the scene to the main menu
    }
}
