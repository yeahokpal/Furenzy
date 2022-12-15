/*
 * Programmer: Sliman / Jack
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public SaveSystem save;

    private void Awake()
    {
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
        save = GameObject.Find("SaveManager").GetComponent<SaveSystem>();
    }

    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
    }

    public void LevelCleared() // When All Enemies are Dead, go back to the hub with the new data
    {
        int i = 0;
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            ++i;
        }
        if (i == 0)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level_1":
                    save.Write("save", "cleared", 1, "1");
                    break;
                case "Level_2":
                    save.Write("save", "cleared", 1, "1");
                    break;
                case "Level_3":
                    save.Write("save", "cleared", 1, "1");
                    break;
            }
        }
    }
}
