using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    GameObject playerInput;

    public GameObject PlaceholderCharacter;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    private void Start()
    {
        playerInput = PlaceholderCharacter;
    }

    private void Update()
    {
        if (gameObject.GetComponent<PlayerInputManager>().playerCount == 0)
        {
            playerInput = Player1;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        else if (gameObject.GetComponent<PlayerInputManager>().playerCount == 1)
        {
            playerInput = Player2;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        else if (gameObject.GetComponent<PlayerInputManager>().playerCount == 2)
        {
            playerInput = Player3;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        else if (gameObject.GetComponent<PlayerInputManager>().playerCount == 3)
        {
            playerInput = Player4;
            gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
        }
        /*else
        {
            some sort of error message
        }*/
    }

    public void OnPlayerJoined()
    {
        
    }
}
