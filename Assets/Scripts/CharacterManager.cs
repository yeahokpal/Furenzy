using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    GameObject playerInput;

    public GameObject Fox;
    public GameObject Bunny;

    private void Start()
    {
        playerInput = Fox;
    }

    private void Update()
    {
        if (gameObject.GetComponent<PlayerInputManager>().playerCount == 0)
            playerInput = Fox;
        else
            playerInput = Bunny;
        gameObject.GetComponent<PlayerInputManager>().playerPrefab = playerInput;
    }

    public void OnPlayerJoined()
    {
        
    }
}
