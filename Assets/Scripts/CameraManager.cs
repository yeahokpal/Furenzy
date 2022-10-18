using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    float foxX = 0, foxY = 0, bunnyX = 0, bunnyY = 0, birdX = 0, birdY = 0, ferretX = 0, ferretY = 0, camX = 0, camY = 0;
    int numOfPlayers;
    public GameObject Fox, Bunny, Bird, Ferret;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        numOfPlayers = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>().playerCount;

        // If the player is in the scene, then initialize their variable
        if (GameObject.Find("Fox(Clone)"))
        {
            Fox = GameObject.Find("Fox(Clone)");
            foxX = Fox.GetComponent<Transform>().position.x;
            foxY = Fox.GetComponent<Transform>().position.y;
        }
            
        if (GameObject.Find("Bunny(Clone)"))
        {
            Bunny = GameObject.Find("Bunny(Clone)");
            bunnyX = Bunny.GetComponent<Transform>().position.x;
            bunnyY = Bunny.GetComponent<Transform>().position.y;
        }
            
        if (GameObject.Find("Bird(Clone)"))
        {
            Bird = GameObject.Find("Bird(Clone)");
            birdX = Bird.GetComponent<Transform>().position.x;
            birdY = Bird.GetComponent<Transform>().position.y;
        }
            
        if (GameObject.Find("Ferret(Clone)"))
        {
            Ferret = GameObject.Find("Ferret(Clone)");
            ferretX = Ferret.GetComponent<Transform>().position.x;
            ferretY = Ferret.GetComponent<Transform>().position.y;
        }

        if (GameObject.Find("Fox(Clone)") || GameObject.Find("Bunny(Clone)") || GameObject.Find("Bird(Clone)") || GameObject.Find("Ferret(Clone)"))
        {
            camX = (foxX + bunnyX + birdX + ferretX) / numOfPlayers;
            camY = (foxY + bunnyY + birdY + ferretY) / numOfPlayers;
        }

        transform.position = new Vector2(camX, camY);
    }
}
