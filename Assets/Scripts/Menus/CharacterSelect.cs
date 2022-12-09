/*
 * Programmer: Caden
 * Purpose: Make player select screen functional
 * Input: Player button presses
 * Output: Each player's character choice
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region Variables
    
    //this looks ridiculous but i promise its necessary
    public GameObject P1_Join, P2_Join, P3_Join, P4_Join;
    public GameObject P1_Up, P2_Up, P3_Up, P4_Up;
    public GameObject P1_Down, P2_Down, P3_Down, P4_Down;
    public GameObject P1_Fox, P2_Fox, P3_Fox, P4_Fox;
    public GameObject P1_Bunny, P2_Bunny, P3_Bunny, P4_Bunny;
    public GameObject P1_Bird, P2_Bird, P3_Bird, P4_Bird;
    public GameObject P1_Ferret, P2_Ferret, P3_Ferret, P4_Ferret;
    public GameObject P1_Prefab, P2_Prefab, P3_Prefab, P4_Prefab;
    public GameObject P1_Manager, P2_Manager, P3_Manager, P4_Manager;
    public GameObject P1_Check, P2_Check, P3_Check, P4_Check;

    public PlayerInputManager InputManager;

    private int numOfPlayers = 0;
    private int P1_Active_Sprite = 1, P2_Active_Sprite = 1, P3_Active_Sprite = 1, P4_Active_Sprite = 1;

    #endregion

    #region Default Methods

    private void Awake()
    {
        //creating a variable from player input manager
        InputManager = GameObject.Find("PlayerSelectManager").GetComponent<PlayerInputManager>();
        Debug.Log("Initial Player Count: " + numOfPlayers);
    }

    #endregion

    #region Custom Methods

    public void PlayerJoined()
    {
        //updates player count when a control joins
        numOfPlayers = InputManager.playerCount;

        Debug.Log("Number of Players: " + numOfPlayers);

        //removes placeholder objects if there are players joined and activates default player sprite
        switch (numOfPlayers)
        {
            //1 player
            case 1:
                //makes sure each player manager knows what canvas it is in control of.  same for other super long lines in this statement
                GameObject.Find("P1_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P1_Manager.GetComponent<InputSystemUIInputModule>();
                P1_Join.SetActive(false);
                P1_Up.SetActive(true);
                P1_Down.SetActive(true);
                P1_Fox.SetActive(true);
                InputManager.playerPrefab = P2_Prefab;
                break;
            //2 players
            case 2:
                GameObject.Find("P2_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P2_Manager.GetComponent<InputSystemUIInputModule>();
                P2_Join.SetActive(false);
                P2_Up.SetActive(true);
                P2_Down.SetActive(true);
                P2_Bunny.SetActive(true);
                InputManager.playerPrefab = P3_Prefab;
                break;
            //3 players
            case 3:
                GameObject.Find("P3_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P3_Manager.GetComponent<InputSystemUIInputModule>();
                P3_Join.SetActive(false);
                P3_Up.SetActive(true);
                P3_Down.SetActive(true);
                P3_Bird.SetActive(true);
                InputManager.playerPrefab = P4_Prefab;
                break;
            //4 players
            case 4:
                GameObject.Find("P4_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P4_Manager.GetComponent<InputSystemUIInputModule>();
                InputManager.DisableJoining();
                P4_Join.SetActive(false);
                P4_Up.SetActive(true);
                P4_Down.SetActive(true);
                P4_Ferret.SetActive(true);
                break;
        }
    }

    public void P1_Down_Click()//player 1 presses down arrow
    {
        ++P1_Active_Sprite;
        //sets correct player sprite active according to P1_Active_Sprite
        switch (P1_Active_Sprite)
        {
            case 1:
                P1_Fox.SetActive(true);
                P1_Bunny.SetActive(false);
                P1_Bird.SetActive(false);
                P1_Ferret.SetActive(false);              
                break;
            case 2:
                P1_Fox.SetActive(false);
                P1_Bunny.SetActive(true);
                P1_Bird.SetActive(false);
                P1_Ferret.SetActive(false);
                break;
            case 3:
                P1_Fox.SetActive(false);
                P1_Bunny.SetActive(false);
                P1_Bird.SetActive(true);
                P1_Ferret.SetActive(false);
                break;
            case 4:
                P1_Fox.SetActive(false);
                P1_Bunny.SetActive(false);
                P1_Bird.SetActive(false);
                P1_Ferret.SetActive(true);
                P1_Active_Sprite = 0;//makes variable 0 so that next time button is pressed it equals 1
                break;
            default:
                Debug.Log("invalid player number " + P1_Active_Sprite);
                break;
        }
        Debug.Log("P1 Active Sprite: " + P1_Active_Sprite);
    }

    public void TestClick()
    {
        Debug.Log("Click");
        P1_Check.SetActive(true);
    }

    #endregion
}
