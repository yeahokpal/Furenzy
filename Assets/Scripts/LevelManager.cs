/*
 * Programmer: Jack / Slimane
 * Purpose: To Determine when to change scene
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public SaveSystem save;
    public GameObject WinText;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            GameObject.Find("Keyhole1").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Keyhole1").GetComponent<Interactable>().oldSprite;
            GameObject.Find("Keyhole2").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Keyhole2").GetComponent<Interactable>().oldSprite;
            GameObject.Find("Keyhole3").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Keyhole3").GetComponent<Interactable>().oldSprite;
            //GameObject.Find("KeyText").GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.Find("KeyText").GetComponent<TextMeshProUGUI>().text) - 5).ToString(); ;
        }

        WinText.SetActive(false);
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
        save = GameObject.Find("SaveManager").GetComponent<SaveSystem>();
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
        Debug.Log(i);
        if (i == 0)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level_1":
                    save.Write("save", "cleared", 1, "1");
                    break;
                case "Level_2":
                    save.Write("save", "cleared", 2, "1");
                    break;
                case "Level_3":
                    save.Write("save", "cleared", 3, "1");
                    break;
            }
            StartCoroutine(WaitAndLoadHub());
        }

        i = 0;
        if (GameObject.Find("Fox(Clone)"))
            ++i;
        if (GameObject.Find("Bunny(Clone)"))
            ++i;
        if (GameObject.Find("Bird(Clone)"))
            ++i;
        if (GameObject.Find("Ferret(Clone)"))
            ++i;


        if (i == 0)
        {
            GameOver();
        }
    }

    public void LoadMenu()
    {
        if (GameObject.Find("P1_Menu_Controls(Clone)"))
            Destroy(GameObject.Find("P1_Menu_Controls(Clone)"));
        if (GameObject.Find("P2_Menu_Controls(Clone)"))
            Destroy(GameObject.Find("P2_Menu_Controls(Clone)"));
        if (GameObject.Find("P3_Menu_Controls(Clone)"))
            Destroy(GameObject.Find("P3_Menu_Controls(Clone)"));
        if (GameObject.Find("P4_Menu_Controls(Clone)"))
            Destroy(GameObject.Find("P4_Menu_Controls(Clone)"));
        Destroy(GameObject.Find("SaveManager"));
        Destroy(GameObject.Find("PlayerSelectManager"));
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitAndLoadHub()
    {
        WinText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HubWorld");
        if (GameObject.Find("Fox(Clone)"))
            GameObject.Find("Fox(Clone)").transform.position = new Vector3(0f, 0f, 0f);
        if (GameObject.Find("Bunny(Clone)"))
            GameObject.Find("Bunny(Clone)").transform.position = new Vector3(0f, 0f, 0f);
        if (GameObject.Find("Bird(Clone)"))
            GameObject.Find("Bird(Clone)").transform.position = new Vector3(0f, 0f, 0f);
        if (GameObject.Find("Ferret(Clone)"))
            GameObject.Find("Ferret(Clone)").transform.position = new Vector3(0f, 0f, 0f);
    }
}
