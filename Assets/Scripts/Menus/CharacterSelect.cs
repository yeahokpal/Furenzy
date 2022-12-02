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

    int numOfPlayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemovePlayerJoinObjects()
    {
        numOfPlayers = GameObject.Find("PlayerSelectManager").GetComponent<PlayerInputManager>().playerCount;

        Debug.Log(numOfPlayers);

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
