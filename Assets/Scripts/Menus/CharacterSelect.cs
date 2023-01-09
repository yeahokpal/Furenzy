/*
 * Programmer: Caden
 * Purpose: Make player select screen functional
 * Input: Player button presses
 * Output: Each player's character choice
 */

using System.Collections;
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
    public GameObject P1_OK, P2_OK, P3_OK, P4_OK;
    public GameObject Character_Select_Error;
    public GameObject Fox_Character, Bunny_Character, Bird_Character, Ferret_Character;
    public PlayerInputManager InputManager;
    private int numOfPlayers = 0, readyPlayers = 0;
    private int P1_Character = 20, P2_Character = 200, P3_Character = 2000, P4_Character = 20000;
    private int P1_Active_Sprite = 1, P2_Active_Sprite = 2, P3_Active_Sprite = 3, P4_Active_Sprite = 4;

    #endregion

    #region Default Methods

    private void Awake()
    {
        //creating a variable from player input manager
        InputManager = GameObject.Find("PlayerSelectManager").GetComponent<PlayerInputManager>();
        //Debug.Log("Initial Player Count: " + numOfPlayers);
    }

    #endregion

    #region Custom Methods

    public void PlayerJoined()//happens when a player joins scene
    {
        //updates player count when a control joins
        numOfPlayers = InputManager.playerCount;

        //Debug.Log("Number of Players: " + numOfPlayers);

        //removes placeholder objects if there are players joined and activates default player sprite
        switch (numOfPlayers)
        {
            //1 player
            case 1:
                //makes sure each player manager knows what canvas it is in control of.  same for other super long lines in this statement
                GameObject.Find("P1_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P1_Manager.GetComponent<InputSystemUIInputModule>();
                P1_OK.SetActive(true);
                P1_Join.SetActive(false);
                P1_Up.SetActive(true);
                P1_Down.SetActive(true);
                P1_Fox.SetActive(true);
                InputManager.playerPrefab = P2_Prefab;
                break;
            //2 players
            case 2:
                GameObject.Find("P2_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P2_Manager.GetComponent<InputSystemUIInputModule>();
                P2_OK.SetActive(true);
                P2_Join.SetActive(false);
                P2_Up.SetActive(true);
                P2_Down.SetActive(true);
                P2_Bunny.SetActive(true);
                InputManager.playerPrefab = P3_Prefab;
                break;
            //3 players
            case 3:
                GameObject.Find("P3_Menu_Controls(Clone)").GetComponent<PlayerInput>().uiInputModule = P3_Manager.GetComponent<InputSystemUIInputModule>();
                P3_OK.SetActive(true);
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
                P4_OK.SetActive(true);
                P4_Join.SetActive(false);
                P4_Up.SetActive(true);
                P4_Down.SetActive(true);
                P4_Ferret.SetActive(true);
                break;
        }
    }

    public void VerifyPlayers()//runs everytime a player presses "OK!" button
    {
        if(readyPlayers == InputManager.playerCount)
        {
            if ((P1_Character == P2_Character) || (P1_Character == P3_Character) || (P1_Character == P4_Character || P2_Character == P3_Character || P2_Character == P4_Character || P3_Character == P4_Character))
            {
                readyPlayers = 0;
                P1_Check.SetActive(false);
                P2_Check.SetActive(false);
                P3_Check.SetActive(false);
                P4_Check.SetActive(false);
                switch (InputManager.playerCount)
                {
                    case 1:
                        P1_Up.SetActive(true);
                        P1_Down.SetActive(true);
                        break;
                    case 2:
                        P1_Up.SetActive(true);
                        P1_Down.SetActive(true);
                        P2_Up.SetActive(true);
                        P2_Down.SetActive(true);
                        break;
                    case 3:
                        P1_Up.SetActive(true);
                        P1_Down.SetActive(true);
                        P2_Up.SetActive(true);
                        P2_Down.SetActive(true);
                        P3_Up.SetActive(true);
                        P3_Down.SetActive(true);
                        break;
                    case 4:
                        P1_Up.SetActive(true);
                        P1_Down.SetActive(true);
                        P2_Up.SetActive(true);
                        P2_Down.SetActive(true);
                        P3_Up.SetActive(true);
                        P3_Down.SetActive(true);
                        P4_Up.SetActive(true);
                        P4_Down.SetActive(true);
                        break;
                }
                P1_Up.SetActive(true);
                P1_Down.SetActive(true);
                P2_Up.SetActive(true);
                Character_Select_Error.SetActive(true);
                StartCoroutine(Delay());
            }
            else
            {
                if (GameObject.Find("P1_Menu_Controls(Clone)") != null)
                {
                    switch (P1_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, GameObject.Find("P1_Menu_Controls(Clone)").transform);
                            break;
                        case 2:
                            Instantiate(Bunny_Character, GameObject.Find("P1_Menu_Controls(Clone)").transform);
                            break;
                        case 3:
                            Instantiate(Bird_Character, GameObject.Find("P1_Menu_Controls(Clone)").transform);
                            break;
                        case 4:
                            Instantiate(Ferret_Character, GameObject.Find("P1_Menu_Controls(Clone)").transform);
                            break;
                    }
                    DontDestroyOnLoad(GameObject.Find("P1_Menu_Controls(Clone)"));
                }
                    
                if (GameObject.Find("P2_Menu_Controls(Clone)") != null)
                {
                    switch (P2_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, GameObject.Find("P2_Menu_Controls(Clone)").transform);
                            break;
                        case 2:
                            Instantiate(Bunny_Character, GameObject.Find("P2_Menu_Controls(Clone)").transform);
                            break;
                        case 3:
                            Instantiate(Bird_Character, GameObject.Find("P2_Menu_Controls(Clone)").transform);
                            break;
                        case 4:
                            Instantiate(Ferret_Character, GameObject.Find("P2_Menu_Controls(Clone)").transform);
                            break;
                    }
                    DontDestroyOnLoad(GameObject.Find("P2_Menu_Controls(Clone)"));
                }
                    
                if (GameObject.Find("P3_Menu_Controls(Clone)") != null)
                {
                    switch (P3_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, GameObject.Find("P3_Menu_Controls(Clone)").transform);
                            break;
                        case 2:
                            Instantiate(Bunny_Character, GameObject.Find("P3_Menu_Controls(Clone)").transform);
                            break;
                        case 3:
                            Instantiate(Bird_Character, GameObject.Find("P3_Menu_Controls(Clone)").transform);
                            break;
                        case 4:
                            Instantiate(Ferret_Character, GameObject.Find("P3_Menu_Controls(Clone)").transform);
                            break;
                    }
                    DontDestroyOnLoad(GameObject.Find("P3_Menu_Controls(Clone)"));
                }
                    
                if (GameObject.Find("P4_Menu_Controls(Clone)") != null)
                {
                    switch (P4_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, GameObject.Find("P4_Menu_Controls(Clone)").transform);
                            break;
                        case 2:
                            Instantiate(Bunny_Character, GameObject.Find("P4_Menu_Controls(Clone)").transform);
                            break;
                        case 3:
                            Instantiate(Bird_Character, GameObject.Find("P4_Menu_Controls(Clone)").transform);
                            break;
                        case 4:
                            Instantiate(Ferret_Character, GameObject.Find("P4_Menu_Controls(Clone)").transform);
                            break;
                    }
                    DontDestroyOnLoad(GameObject.Find("P4_Menu_Controls(Clone)"));
                }

                DontDestroyOnLoad(InputManager);
                SceneManager.LoadScene("HubWorld");
            }
        }
    }

    public IEnumerator Delay()//just used to display error to player
    {
        yield return new WaitForSeconds(3f);
        Character_Select_Error.SetActive(false);
    }

    #region P1 Button Click Methods
    public void P1_Down_Click()//player 1 presses down arrow
    {
        ++P1_Active_Sprite;
        if (P1_Active_Sprite == 5)
            P1_Active_Sprite = 1;
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
                break;
            default:
                Debug.Log("invalid player number " + P1_Active_Sprite);
                break;
        }
        Debug.Log("P1 Active Sprite: " + P1_Active_Sprite);
    }

    public void P1_Up_Click()//player 1 presses up arrow
    {
        --P1_Active_Sprite;
        if (P1_Active_Sprite == 0)
            P1_Active_Sprite = 4;
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
                break;
            default:
                Debug.Log("invalid player number " + P1_Active_Sprite);
                break;
        }
        Debug.Log("P1 Active Sprite: " + P1_Active_Sprite);
    }

    public void P1_OK_Click()
    {
        if (P1_Check.activeSelf == false)
        {
            ++readyPlayers;
            P1_Check.SetActive(true);
            P1_Up.SetActive(false);
            P1_Down.SetActive(false);
            if (P1_Fox.activeSelf == true)
                P1_Character = 1;
            if (P1_Bunny.activeSelf == true)
                P1_Character = 2;
            if (P1_Bird.activeSelf == true)
                P1_Character = 3;
            if (P1_Ferret.activeSelf == true)
                P1_Character = 4;
            VerifyPlayers();
        }      
    }

    #endregion

    #region P2 Button Click Methods
    public void P2_Down_Click()//player 2 presses down arrow
    {
        ++P2_Active_Sprite;
        if (P2_Active_Sprite == 5)
            P2_Active_Sprite = 1;
        //sets correct player sprite active according to P2_Active_Sprite
        switch (P2_Active_Sprite)
        {
            case 1:
                P2_Fox.SetActive(true);
                P2_Bunny.SetActive(false);
                P2_Bird.SetActive(false);
                P2_Ferret.SetActive(false);
                break;
            case 2:
                P2_Fox.SetActive(false);
                P2_Bunny.SetActive(true);
                P2_Bird.SetActive(false);
                P2_Ferret.SetActive(false);
                break;
            case 3:
                P2_Fox.SetActive(false);
                P2_Bunny.SetActive(false);
                P2_Bird.SetActive(true);
                P2_Ferret.SetActive(false);
                break;
            case 4:
                P2_Fox.SetActive(false);
                P2_Bunny.SetActive(false);
                P2_Bird.SetActive(false);
                P2_Ferret.SetActive(true);
                break;
            default:
                Debug.Log("invalid player number " + P2_Active_Sprite);
                break;
        }
        Debug.Log("P2 Active Sprite: " + P2_Active_Sprite);
    }

    public void P2_Up_Click()//player 2 presses up arrow
    {
        --P2_Active_Sprite;
        if (P2_Active_Sprite == 0)
            P2_Active_Sprite = 4;
        //sets correct player sprite active according to P2_Active_Sprite
        switch (P2_Active_Sprite)
        {
            case 1:
                P2_Fox.SetActive(true);
                P2_Bunny.SetActive(false);
                P2_Bird.SetActive(false);
                P2_Ferret.SetActive(false);
                break;
            case 2:
                P2_Fox.SetActive(false);
                P2_Bunny.SetActive(true);
                P2_Bird.SetActive(false);
                P2_Ferret.SetActive(false);
                break;
            case 3:
                P2_Fox.SetActive(false);
                P2_Bunny.SetActive(false);
                P2_Bird.SetActive(true);
                P2_Ferret.SetActive(false);
                break;
            case 4:
                P2_Fox.SetActive(false);
                P2_Bunny.SetActive(false);
                P2_Bird.SetActive(false);
                P2_Ferret.SetActive(true);
                break;
            default:
                Debug.Log("invalid player number " + P2_Active_Sprite);
                break;
        }
        Debug.Log("P2 Active Sprite: " + P2_Active_Sprite);
    }

    public void P2_OK_Click()
    {
        if (P2_Check.activeSelf == false)
        {
            ++readyPlayers;
            P2_Check.SetActive(true);
            P2_Up.SetActive(false);
            P2_Down.SetActive(false);
            if (P2_Fox.activeSelf == true)
                P2_Character = 1;
            if (P2_Bunny.activeSelf == true)
                P2_Character = 2;
            if (P2_Bird.activeSelf == true)
                P2_Character = 3;
            if (P2_Ferret.activeSelf == true)
                P2_Character = 4;
            VerifyPlayers();
        }     
    }

    #endregion

    #region P3 Button Click Methods
    public void P3_Down_Click()//player 3 presses down arrow
    {
        ++P3_Active_Sprite;
        if (P3_Active_Sprite == 5)
            P3_Active_Sprite = 1;
        //sets correct player sprite active according to P3_Active_Sprite
        switch (P3_Active_Sprite)
        {
            case 1:
                P3_Fox.SetActive(true);
                P3_Bunny.SetActive(false);
                P3_Bird.SetActive(false);
                P3_Ferret.SetActive(false);
                break;
            case 2:
                P3_Fox.SetActive(false);
                P3_Bunny.SetActive(true);
                P3_Bird.SetActive(false);
                P3_Ferret.SetActive(false);
                break;
            case 3:
                P3_Fox.SetActive(false);
                P3_Bunny.SetActive(false);
                P3_Bird.SetActive(true);
                P3_Ferret.SetActive(false);
                break;
            case 4:
                P3_Fox.SetActive(false);
                P3_Bunny.SetActive(false);
                P3_Bird.SetActive(false);
                P3_Ferret.SetActive(true);
                break;
            default:
                Debug.Log("invalid player number " + P3_Active_Sprite);
                break;
        }
        Debug.Log("P3 Active Sprite: " + P3_Active_Sprite);
    }

    public void P3_Up_Click()//player 3 presses up arrow
    {
        --P3_Active_Sprite;
        if (P3_Active_Sprite == 0)
            P3_Active_Sprite = 4;
        //sets correct player sprite active according to P3_Active_Sprite
        switch (P3_Active_Sprite)
        {
            case 1:
                P3_Fox.SetActive(true);
                P3_Bunny.SetActive(false);
                P3_Bird.SetActive(false);
                P3_Ferret.SetActive(false);
                break;
            case 2:
                P3_Fox.SetActive(false);
                P3_Bunny.SetActive(true);
                P3_Bird.SetActive(false);
                P3_Ferret.SetActive(false);
                break;
            case 3:
                P3_Fox.SetActive(false);
                P3_Bunny.SetActive(false);
                P3_Bird.SetActive(true);
                P3_Ferret.SetActive(false);
                break;
            case 4:
                P3_Fox.SetActive(false);
                P3_Bunny.SetActive(false);
                P3_Bird.SetActive(false);
                P3_Ferret.SetActive(true);
                break;
            default:
                Debug.Log("invalid player number " + P3_Active_Sprite);
                break;
        }
        Debug.Log("P3 Active Sprite: " + P3_Active_Sprite);
    }

    public void P3_OK_Click()
    {
        if (P3_Check.activeSelf == false)
        {
            ++readyPlayers;
            P3_Check.SetActive(true);
            P3_Up.SetActive(false);
            P3_Down.SetActive(false);
            if (P3_Fox.activeSelf == true)
                P3_Character = 1;
            if (P3_Bunny.activeSelf == true)
                P3_Character = 2;
            if (P3_Bird.activeSelf == true)
                P3_Character = 3;
            if (P3_Ferret.activeSelf == true)
                P3_Character = 4;
            VerifyPlayers();
        }      
    }

    #endregion

    #region P4 Button Click Methods
    public void P4_Down_Click()//player 4 presses down arrow
    {
        ++P4_Active_Sprite;
        if (P4_Active_Sprite == 5)
            P4_Active_Sprite = 1;
        //sets correct player sprite active according to P4_Active_Sprite
        switch (P4_Active_Sprite)
        {
            case 1:
                P4_Fox.SetActive(true);
                P4_Bunny.SetActive(false);
                P4_Bird.SetActive(false);
                P4_Ferret.SetActive(false);
                break;
            case 2:
                P4_Fox.SetActive(false);
                P4_Bunny.SetActive(true);
                P4_Bird.SetActive(false);
                P4_Ferret.SetActive(false);
                break;
            case 3:
                P4_Fox.SetActive(false);
                P4_Bunny.SetActive(false);
                P4_Bird.SetActive(true);
                P4_Ferret.SetActive(false);
                break;
            case 4:
                P4_Fox.SetActive(false);
                P4_Bunny.SetActive(false);
                P4_Bird.SetActive(false);
                P4_Ferret.SetActive(true);
                break;
            default:
                Debug.Log("invalid player number " + P4_Active_Sprite);
                break;
        }
        Debug.Log("P4 Active Sprite: " + P4_Active_Sprite);
    }

    public void P4_Up_Click()//player 4 presses up arrow
    {
        --P4_Active_Sprite;
        if (P4_Active_Sprite == 0)
            P4_Active_Sprite = 4;
        //sets correct player sprite active according to P4_Active_Sprite
        switch (P4_Active_Sprite)
        {
            case 1:
                P4_Fox.SetActive(true);
                P4_Bunny.SetActive(false);
                P4_Bird.SetActive(false);
                P4_Ferret.SetActive(false);
                break;
            case 2:
                P4_Fox.SetActive(false);
                P4_Bunny.SetActive(true);
                P4_Bird.SetActive(false);
                P4_Ferret.SetActive(false);
                break;
            case 3:
                P4_Fox.SetActive(false);
                P4_Bunny.SetActive(false);
                P4_Bird.SetActive(true);
                P4_Ferret.SetActive(false);
                break;
            case 4:
                P4_Fox.SetActive(false);
                P4_Bunny.SetActive(false);
                P4_Bird.SetActive(false);
                P4_Ferret.SetActive(true);
                break;
            default:
                Debug.Log("invalid player number " + P4_Active_Sprite);
                break;
        }
        Debug.Log("P4 Active Sprite: " + P4_Active_Sprite);
    }

    public void P4_OK_Click()
    {
        if (P4_Check.activeSelf == false)
        {
            ++readyPlayers;
            P4_Check.SetActive(true);
            P4_Up.SetActive(false);
            P4_Down.SetActive(false);
            if (P4_Fox.activeSelf == true)
                P4_Character = 1;
            if (P4_Bunny.activeSelf == true)
                P4_Character = 2;
            if (P4_Bird.activeSelf == true)
                P4_Character = 3;
            if (P4_Ferret.activeSelf == true)
                P4_Character = 4;
            VerifyPlayers();
        }
    }      

    #endregion

    #endregion
}
