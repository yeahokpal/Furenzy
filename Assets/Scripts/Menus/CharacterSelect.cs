/*
 * Programmer: Caden
 * Purpose: Make player select screen functional
 * Input: Player button presses
 * Output: Each player's character choice
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region Variables
    
    //this looks ridiculous but i promise its necessary
    [SerializeField] private GameObject P1_Join, P2_Join, P3_Join, P4_Join;
    [SerializeField] private GameObject P1_Up, P2_Up, P3_Up, P4_Up;
    [SerializeField] private GameObject P1_Down, P2_Down, P3_Down, P4_Down;
    [SerializeField] private GameObject P1_Fox, P2_Fox, P3_Fox, P4_Fox;
    [SerializeField] private GameObject P1_Bunny, P2_Bunny, P3_Bunny, P4_Bunny;
    [SerializeField] private GameObject P1_Bird, P2_Bird, P3_Bird, P4_Bird;
    [SerializeField] private GameObject P1_Ferret, P2_Ferret, P3_Ferret, P4_Ferret;
    [SerializeField] private GameObject P1_Prefab, P2_Prefab, P3_Prefab, P4_Prefab;
    [SerializeField] private GameObject P1_Manager, P2_Manager, P3_Manager, P4_Manager;
    [SerializeField] private GameObject P1_Check, P2_Check, P3_Check, P4_Check;
    [SerializeField] private GameObject P1_OK, P2_OK, P3_OK, P4_OK;
    [SerializeField] private GameObject Character_Select_Error;
    [SerializeField] private GameObject Fox_Character, Bunny_Character, Bird_Character, Ferret_Character;
    [SerializeField] private InputActionAsset Player_Actions_1, Player_Actions_2, Player_Actions_3, Player_Actions_4;
    [SerializeField] private PlayerInputManager InputManager;
    [NonSerialized] private GameObject Player1, Player2, Player3, Player4;
    [NonSerialized] private int numOfPlayers = 0, readyPlayers = 0;
    [NonSerialized] private int P1_Character = 20, P2_Character = 200, P3_Character = 2000, P4_Character = 20000;
    [NonSerialized] private int P1_Active_Sprite = 1, P2_Active_Sprite = 2, P3_Active_Sprite = 3, P4_Active_Sprite = 4;

    #endregion

    #region Default Methods

    private void Awake()
    {
        //creating a variable from player input manager
        InputManager = GameObject.Find("PlayerSelectManager").GetComponent<PlayerInputManager>();
    }

    #endregion

    #region Custom Methods

    public void PlayerJoined()//happens when a player joins scene
    {
        //updates player count when a control joins
        numOfPlayers = InputManager.playerCount;

        //removes placeholder objects if there are players joined and activates default player sprite
        switch (numOfPlayers)
        {
            //1 player
            case 1:
                //makes sure each player manager knows what canvas it is in control of.  same for other super long lines in this statement
                Player1 = GameObject.Find("P1_Menu_Controls(Clone)");
                Player1.GetComponent<PlayerInput>().uiInputModule = P1_Manager.GetComponent<InputSystemUIInputModule>();
                P1_OK.SetActive(true);
                P1_Join.SetActive(false);
                P1_Up.SetActive(true);
                P1_Down.SetActive(true);
                P1_Fox.SetActive(true);
                InputManager.playerPrefab = P2_Prefab;
                break;
            //2 players
            case 2:
                Player2 = GameObject.Find("P2_Menu_Controls(Clone)");
                Player2.GetComponent<PlayerInput>().uiInputModule = P2_Manager.GetComponent<InputSystemUIInputModule>();
                P2_OK.SetActive(true);
                P2_Join.SetActive(false);
                P2_Up.SetActive(true);
                P2_Down.SetActive(true);
                P2_Bunny.SetActive(true);
                InputManager.playerPrefab = P3_Prefab;
                break;
            //3 players
            case 3:
                Player3 = GameObject.Find("P3_Menu_Controls(Clone)");
                Player3.GetComponent<PlayerInput>().uiInputModule = P3_Manager.GetComponent<InputSystemUIInputModule>();
                P3_OK.SetActive(true);
                P3_Join.SetActive(false);
                P3_Up.SetActive(true);
                P3_Down.SetActive(true);
                P3_Bird.SetActive(true);
                InputManager.playerPrefab = P4_Prefab;
                break;
            //4 players
            case 4:
                Player4 = GameObject.Find("P4_Menu_Controls(Clone)");
                Player4.GetComponent<PlayerInput>().uiInputModule = P4_Manager.GetComponent<InputSystemUIInputModule>();
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
                if (Player1 != null)
                {
                    switch (P1_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, Player1.transform);
                            CreateChild(Player1, 1);
                            Player1.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Fox");
                            break;
                        case 2:
                            Instantiate(Bunny_Character, Player1.transform);
                            CreateChild(Player1, 1);
                            Player1.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bunny");
                            break;
                        case 3:
                            Instantiate(Bird_Character, Player1.transform);
                            CreateChild(Player1, 1);
                            Player1.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bird");
                            break;
                        case 4:
                            Instantiate(Ferret_Character, Player1.transform);
                            CreateChild(Player1, 1);
                            Player1.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Ferret");
                            break;
                    }
                    DontDestroyOnLoad(Player1);
                }

                if (Player2 != null)
                {
                    switch (P2_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, Player2.transform);
                            CreateChild(Player2, 2);
                            Player2.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Fox");
                            break;
                        case 2:
                            Instantiate(Bunny_Character, Player2.transform);
                            CreateChild(Player2, 2);
                            Player2.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bunny");
                            break;
                        case 3:
                            Instantiate(Bird_Character, Player2.transform);
                            CreateChild(Player2, 2);
                            Player2.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bird");
                            break;
                        case 4:
                            Instantiate(Ferret_Character, Player2.transform);
                            CreateChild(Player2, 2);
                            Player2.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Ferret");
                            break;
                    }
                    DontDestroyOnLoad(Player2);
                }

                if (Player3 != null)
                {
                    switch (P3_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, Player3.transform);
                            CreateChild(Player3, 3);
                            Player3.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Fox");
                            break;
                        case 2:
                            Instantiate(Bunny_Character, Player3.transform);
                            CreateChild(Player3, 3);
                            Player3.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bunny");
                            break;
                        case 3:
                            Instantiate(Bird_Character, Player3.transform);
                            CreateChild(Player3, 3);
                            Player3.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bird");
                            break;
                        case 4:
                            Instantiate(Ferret_Character, Player3.transform);
                            CreateChild(Player3, 3);
                            Player3.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Ferret");
                            break;
                    }
                    DontDestroyOnLoad(Player3);
                }

                if (Player4 != null)
                {
                    switch (P4_Character)
                    {
                        case 1:
                            Instantiate(Fox_Character, Player4.transform);
                            CreateChild(Player4, 4);
                            Player4.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Fox");
                            break;
                        case 2:
                            Instantiate(Bunny_Character, Player4.transform);
                            CreateChild(Player4, 4);
                            Player4.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bunny");
                            break;
                        case 3:
                            Instantiate(Bird_Character, Player4.transform);
                            CreateChild(Player4, 4);
                            Player4.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Bird");
                            break;
                        case 4:
                            Instantiate(Ferret_Character, Player4.transform);
                            CreateChild(Player4, 4);
                            Player4.transform.GetChild(0).GetComponent<PlayerInput>().SwitchCurrentActionMap("Ferret");
                            break;
                    }
                    DontDestroyOnLoad(Player4);
                }
                DontDestroyOnLoad(InputManager);
                SceneManager.LoadScene("HubWorld");
            }
        }
    }

    private void CreateChild(GameObject Player, int Num)//Adds child to object from this scene
    {
        GameObject Child = Player.transform.GetChild(0).gameObject;
        PlayerInput Input = Player.GetComponent<PlayerInput>();
        Destroy(Player.GetComponent<PlayerInput>());
        Child.AddComponent<PlayerInput>().Equals(Input);
        switch (Num)
        {
            case 1:
                Child.GetComponent<PlayerInput>().actions = Player_Actions_1;
                break;
            case 2:
                Child.GetComponent<PlayerInput>().actions = Player_Actions_2;
                break;
            case 3:
                Child.GetComponent<PlayerInput>().actions = Player_Actions_3;
                break;
            case 4:
                Child.GetComponent<PlayerInput>().actions = Player_Actions_4;
                break;
        }
        //Child.GetComponent<PlayerInput>().actions = PlayerActions;
        Player.GetComponentInChildren<PlayerManager>().enabled = true;
            
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
