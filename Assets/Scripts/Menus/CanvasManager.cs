using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public int enemyCount = 0;
    bool canCheckForEnemies = false;

    private void Start()
    {
        Resume();

        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            ++enemyCount;
            canCheckForEnemies = true;
        }
    }

    private void Update()
    {
        if (enemyCount == 0 && canCheckForEnemies)
        {
            GameObject.Find("YouWin").SetActive(true);
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
        // Change the scene to the main menu
    }
}
