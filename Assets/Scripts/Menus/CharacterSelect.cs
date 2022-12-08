using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour
{
    #region GameObject Variables
    public GameObject P1_Join, P2_Join, P3_Join, P4_Join;
    public GameObject P1_Up, P2_Up, P3_Up, P4_Up;
    public GameObject P1_Down, P2_Down, P3_Down, P4_Down;
    public GameObject P1_Fox, P2_Fox, P3_Fox, P4_Fox;
    public GameObject P1_Bunny, P2_Bunny, P3_Bunny, P4_Bunny;
    public GameObject P1_Bird, P2_Bird, P3_Bird, P4_Bird;
    public GameObject P1_Ferret, P2_Ferret, P3_Ferret, P4_Ferret;
    public GameObject P1_Prefab, P2_Prefab, P3_Prefab, P4_Prefab;
    public GameObject P1_Manager, P2_Manager, P3_Manager, P4_Manager;
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

        //removes placeholder objects if there are players joined
        switch (numOfPlayers)
        {
            case 1:
                P1_Prefab.GetComponent<PlayerInput>().uiInputModule = P1_Manager.GetComponent<InputSystemUIInputModule>();
                P1_Join.SetActive(false);
                P1_Up.SetActive(true);
                P1_Down.SetActive(true);
                P1_Fox.SetActive(true);
                InputManager.playerPrefab = P2_Prefab;
                break;
            case 2:
                P2_Prefab.GetComponent<PlayerInput>().uiInputModule = P2_Manager.GetComponent<InputSystemUIInputModule>();
                P2_Join.SetActive(false);
                P2_Up.SetActive(true);
                P2_Down.SetActive(true);
                P2_Bunny.SetActive(true);
                InputManager.playerPrefab = P3_Prefab;
                break;
            case 3:
                P3_Prefab.GetComponent<PlayerInput>().uiInputModule = P3_Manager.GetComponent<InputSystemUIInputModule>();
                P3_Join.SetActive(false);
                P3_Up.SetActive(true);
                P3_Down.SetActive(true);
                P3_Bird.SetActive(true);
                InputManager.playerPrefab = P4_Prefab;
                break;
            case 4:
                P4_Prefab.GetComponent<PlayerInput>().uiInputModule = P4_Manager.GetComponent<InputSystemUIInputModule>();
                InputManager.DisableJoining();
                P4_Join.SetActive(false);
                P4_Up.SetActive(true);
                P4_Down.SetActive(true);
                P4_Ferret.SetActive(true);
                break;
        }
    }

    public void TestClick()
    {
        Debug.Log("Click");
    }

    public void NextOrPrevSprite()
    {
        //if(InputManager.)
    }
}
