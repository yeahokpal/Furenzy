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
    public SaveSystem saveManager;

    private void Start()
    {
        Resume();
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveSystem>();
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

        #region Characters
        if (GameObject.Find("Fox(Clone)"))
        {
            FoxManager fox = GameObject.Find("Fox(Clone)").GetComponent<FoxManager>();
            saveManager.Write("Player", "health", 1, fox.Health.ToString());
            saveManager.Write("Player", "mana", 1, fox.mana.ToString());
        }
        if (GameObject.Find("Bunny(Clone)"))
        {
            BunnyManager bunny = GameObject.Find("Bunny(Clone)").GetComponent<BunnyManager>();
            saveManager.Write("Player", "health", 1, bunny.Health.ToString());
            saveManager.Write("Player", "mana", 1, bunny.mana.ToString());
        }
        if (GameObject.Find("Bird(Clone)"))
        {
            BirdManager bird = GameObject.Find("Bird(Clone)").GetComponent<BirdManager>();
            saveManager.Write("Player", "health", 1, bird.Health.ToString());
            saveManager.Write("Player", "mana", 1, bird.mana.ToString());
        }
        if (GameObject.Find("Ferret(Clone)"))
        {
            FerretManager ferret = GameObject.Find("Ferret(Clone)").GetComponent<FerretManager>();
            saveManager.Write("Player", "health", 1, ferret.Health.ToString());
            saveManager.Write("Player", "mana", 1, ferret.mana.ToString());
        }
        #endregion


    }
}
