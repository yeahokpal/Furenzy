/*
 * Programmer: Jack / Slimane
 * Purpose: To Determine when to change scene
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public SaveSystem save;
    public GameObject WinText;

    private void Awake()
    {
        WinText.SetActive(false);
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
        //save = GameObject.Find("SaveManager").GetComponent<SaveSystem>();
    }


    // Toggleing The death Screen when you die
    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
    }

    public void Update() // When All Enemies are Dead, go back to the hub with the new data
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
            StartCoroutine(WaitAndLoadHub());
        }
    }

    IEnumerator WaitAndLoadHub()
    {
        WinText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HubWorld");
    }
}
