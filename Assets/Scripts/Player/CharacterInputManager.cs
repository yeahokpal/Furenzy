using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterInputManager : MonoBehaviour
{
    GameObject playerInput;
    PlayerSelectManager playerSelectManager;
    PlayerInputManager playerInputManager;

    public GameObject PlaceholderCharacter;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject Fox;
    public GameObject Bunny;
    public GameObject Bird;
    public GameObject Ferret;
    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerSelectManager = GameObject.Find("PlayerSelectManager").GetComponent<PlayerSelectManager>();
    }

    private void Update()
    {
        Debug.Log(playerInputManager);
        if (playerInputManager.GetComponent<InputDevice>().device.name.ToString() == playerSelectManager.ControllerNames[1].ToString())
        {
            switch (playerSelectManager.PlayerSprites[1])
            {
                case "Fox":
                    playerInputManager.playerPrefab = Fox;
                    break;
                case "Bunny":
                    playerInputManager.playerPrefab = Bunny;
                    break;
                case "Bird":
                    playerInputManager.playerPrefab = Bird;
                    break;
                case "Ferret":
                    playerInputManager.playerPrefab = Ferret;
                    break;
            }

        }

        if (playerInputManager.playerCount == 0)
        {
            playerInput = Player1;
            playerInputManager.playerPrefab = playerInput;
        }
        else if (playerInputManager.playerCount == 1)
        {
            playerInput = Player2;
            playerInputManager.playerPrefab = playerInput;
        }
        else if (playerInputManager.playerCount == 2)
        {
            playerInput = Player3;
            playerInputManager.playerPrefab = playerInput;
        }
        else if (playerInputManager.playerCount == 3)
        {
            playerInput = Player4;
            playerInputManager.playerPrefab = playerInput;
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
