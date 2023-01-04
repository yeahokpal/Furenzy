using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor.SceneManagement;

public class PlayerSelectManager : MonoBehaviour
{
    public GameObject[] AllCharacterSprites;
    public GameObject PlaceholderCharacter;
    public GameObject Fox, Bunny, Bird, Ferret;

    public List<string> PlayerSprites;
    public InputDevice[] ControllerNames;
    public PlayerInputManager InputManager;

    private void Awake()
    {
        InputManager = GameObject.Find("PlayerSelectManager").GetComponent<PlayerInputManager>();
        ControllerNames = new InputDevice[4];
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        
    }
    public void AddCharactersToList()
    {
        foreach (GameObject sprite in AllCharacterSprites)
        {
            if (sprite.active == true)
            {
                PlayerSprites.Add(sprite.name);
            }
        }
        if (GameObject.Find("P1_Menu_Controls(Clone)")) { ControllerNames[1] = (GameObject.Find("P1_Menu_Controls(Clone)").GetComponent<PlayerInput>().devices[0]); }
        if (GameObject.Find("P2_Menu_Controls(Clone)")) { ControllerNames[2] = (GameObject.Find("P2_Menu_Controls(Clone)").GetComponent<PlayerInput>().devices[0]); }
        if (GameObject.Find("P3_Menu_Controls(Clone)")) { ControllerNames[3] = (GameObject.Find("P3_Menu_Controls(Clone)").GetComponent<PlayerInput>().devices[0]); }
        if (GameObject.Find("P4_Menu_Controls(Clone)")) { ControllerNames[4] = (GameObject.Find("P4_Menu_Controls(Clone)").GetComponent<PlayerInput>().devices[0]); }
        Debug.Log(ControllerNames[1]);
        Debug.Log(ControllerNames[2]);

        InputManager.playerPrefab = PlaceholderCharacter;

        Destroy(GetComponent<PlayerInputManager>());
    }
}
