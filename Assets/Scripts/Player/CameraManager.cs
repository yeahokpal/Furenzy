/*
 * Programmer: Jack
 * Purpose: Making the camera follow the midpoit of all active players
 * Input: Player positional data
 * Output: Camera position / scale
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    float foxX = 0, foxY = 0, bunnyX = 0, bunnyY = 0, birdX = 0, birdY = 0, ferretX = 0, ferretY = 0, camX = 0, camY = 0;
    float foxDist = 0.01f, bunnyDist = 0.01f, birdDist = 0.01f, ferretDist = 0.01f;
    int numOfPlayers = 0;
    public GameObject Fox, Bunny, Bird, Ferret;
    public CinemachineVirtualCamera Camera;
    //float[] distArray = { foxDist, bunnyDist, birdDist, ferretDist};



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numOfPlayers = 0;

        // If the player is in the scene, then initialize their variable
        if (GameObject.Find("Fox(Clone)"))
        {
            Fox = GameObject.Find("Fox(Clone)");
            foxDist = Vector2.Distance(Fox.transform.position, transform.position);
            foxX = Fox.GetComponent<Transform>().position.x;
            foxY = Fox.GetComponent<Transform>().position.y;
            ++numOfPlayers;
        }
        else
        {
            Fox = null;
            foxDist = 0.01f;
            foxX = 0;
            foxY = 0;
        }

        if (GameObject.Find("Bunny(Clone)"))
        {
            Bunny = GameObject.Find("Bunny(Clone)");
            bunnyDist = Vector2.Distance(Bunny.transform.position, transform.position);
            bunnyX = Bunny.GetComponent<Transform>().position.x;
            bunnyY = Bunny.GetComponent<Transform>().position.y;
            ++numOfPlayers;
        }
        else
        {
            Bunny = null;
            bunnyDist = 0.01f;
            bunnyX = 0;
            bunnyY = 0;
        }

        if (GameObject.Find("Bird(Clone)"))
        {
            Bird = GameObject.Find("Bird(Clone)");
            birdDist = Vector2.Distance(Bird.transform.position, transform.position);
            birdX = Bird.GetComponent<Transform>().position.x;
            birdY = Bird.GetComponent<Transform>().position.y;
            ++numOfPlayers;
        }
        else
        {
            Bird = null;
            birdDist = 0.01f;
            birdX = 0;
            birdY = 0;
        }
            

        if (GameObject.Find("Ferret(Clone)"))
        {
            Ferret = GameObject.Find("Ferret(Clone)");
            ferretDist = Vector2.Distance(Ferret.transform.position, transform.position);
            ferretX = Ferret.GetComponent<Transform>().position.x;
            ferretY = Ferret.GetComponent<Transform>().position.y;
            ++numOfPlayers;
        }
        else
        {
            Ferret = null;
            ferretDist = 0.01f;
            ferretX = 0;
            ferretY = 0;
        }

        //Debug.Log(numOfPlayers);

        if (GameObject.Find("Fox(Clone)") || GameObject.Find("Bunny(Clone)") || GameObject.Find("Bird(Clone)") || GameObject.Find("Ferret(Clone)"))
        {
            camX = (foxX + bunnyX + birdX + ferretX) / numOfPlayers;
            camY = (foxY + bunnyY + birdY + ferretY) / numOfPlayers;
        }

        float[] distArray = { foxDist, bunnyDist, birdDist, ferretDist };

        Array.Sort(distArray, (x, y) => y.CompareTo(x));
        ZoomLevelCalculations(distArray);
        transform.position = new Vector2(camX, camY);
    }

    void ZoomLevelCalculations(float[] distArray)
    {
        if (distArray[0] >= 3.5f)
        {
            Camera.m_Lens.OrthographicSize = distArray[0] + 1.5f;
        }
        else
            Camera.m_Lens.OrthographicSize = 5f;
    }
}
