/*
 * Programmer: Jack
 * Purpose: Loading a new scene when a player enters a BoxCollider2D / isTrigger = true
 * Input: Player enters trigger
 * Output: Player enters scene
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scene);
        }
    }
}
