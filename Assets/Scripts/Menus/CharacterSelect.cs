using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour
{
    public GameObject P1_Join;
    public GameObject P2_Join;
    public GameObject P3_Join;
    public GameObject P4_Join;
    public PlayerInputManager InputManager;

    public int numOfPlayers = 0;

    private void Awake()
    {
        InputManager = GameObject.Find("PlayerSelectManager").GetComponent<PlayerInputManager>();
        Debug.Log("Initial Player Count: " + numOfPlayers);
    }

    public void PlayerJoined()
    {
        //updates player count when a control joins
        numOfPlayers = InputManager.playerCount;

        Debug.Log(numOfPlayers);
        
        //removes the "press +" message if there are players joined
        switch (numOfPlayers)
        {
            case 1:
                P1_Join.SetActive(false);
                break;
            case 2:
                P2_Join.SetActive(false);
                break;
            case 3:
                P3_Join.SetActive(false);
                break;
            case 4:
                P4_Join.SetActive(false);
                break;
        }
    }
}
