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
            if (GameObject.Find("Fox(Clone)"))
                GameObject.Find("Fox(Clone)").transform.position = new Vector3(0, 0, 0);
            if (GameObject.Find("Bunny(Clone)"))
                GameObject.Find("Bunny(Clone)").transform.position = new Vector3(0, 0, 0);
            if (GameObject.Find("Bird(Clone)"))
                GameObject.Find("Bird(Clone)").transform.position = new Vector3(0, 0, 0);
            if (GameObject.Find("Ferret(Clone)"))
                GameObject.Find("Ferret(Clone)").transform.position = new Vector3(0, 0, 0);
            SceneManager.LoadScene(scene);
        }
    }
}
