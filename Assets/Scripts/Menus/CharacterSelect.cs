using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour
{
    #region GameObject Variables
    public GameObject P1_Join, P2_Join, P3_Join, P4_Join;
    public GameObject P1_Fox, P2_Fox, P3_Fox, P4_Fox;
    public GameObject P1_Bunny, P2_Bunny, P3_Bunny, P4_Bunny;
    public GameObject P1_Bird, P2_Bird, P3_Bird, P4_Bird;
    public GameObject P1_Ferret, P2_Ferret, P3_Ferret, P4_Ferret;
    #endregion

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
                P1_Fox.SetActive(true);
                break;
            case 2:
                P2_Join.SetActive(false);
                P2_Bunny.SetActive(true);
                break;
            case 3:
                P3_Join.SetActive(false);
                P3_Bird.SetActive(true);
                break;
            case 4:
                InputManager.DisableJoining();
                P4_Join.SetActive(false);
                P4_Ferret.SetActive(true);
                break;
        }
    }

    public void NextOrPrevSprite()
    {
        //if(InputManager.)
    }
}
