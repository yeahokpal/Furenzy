/*
 * Programmer: Sliman
 * Purpose: Turning on / off UI elements or loads scenes when the user presses certain buttons
 * Input: Player presses buttons
 * Output: UI changes / scenes load
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
