using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject win;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        int enemyCount = 0;
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            ++enemyCount;
        }
        if (enemyCount == 0)
        {
            win.SetActive(true);
        }
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
        SceneManager.LoadScene("MainMenu");
        // Add saving to the file
    }
}
