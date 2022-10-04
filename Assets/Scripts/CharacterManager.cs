using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    GameObject playerInput;

    public GameObject Fox;
    public GameObject Bunny;
    public GameObject Bird;

    private void Start()
    {
        playerInput = Fox;
    }

    private void Update()
    {
        if (gameObject.GetComponent<PlayerInputManager>().playerCount == 0)
        {
            playerInput = Fox;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        else if (gameObject.GetComponent<PlayerInputManager>().playerCount == 1)
        {
            playerInput = Bunny;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        else if (gameObject.GetComponent<PlayerInputManager>().playerCount == 2)
        {
            playerInput = Bird;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        /*else if (gameObject.GetComponent<PlayerInputManager>().playerCount == 3)
        {
            playerInput = player4
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        else
        {
            some sort of error message
        }*/
    }

    public void OnPlayerJoined()
    {
        
    }
}
