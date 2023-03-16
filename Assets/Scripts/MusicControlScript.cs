/*
 * Programmer: Sliman
 * Purpose: Manages music settings and keeps the audio to be as the player sets
 * Input: Sound Settings
 * Output: Audio Output
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MusicControlScript : MonoBehaviour
{
    public AudioClip theme;
    public AudioClip lvl1Theme;
    public AudioClip lvl2Theme;
    public AudioClip lvl3Theme;
    public float volume = .5f;
    int levelTracker;
    private void Start()
    {
        Time.timeScale = 1f;
        gameObject.GetComponent<AudioSource>().volume = .5f;
        DontDestroyOnLoad(this);
    }

    public void UpdateVolume()
    {
        volume = GameObject.Find("Slider").GetComponent<Slider>().value;
    }

    private void Update()
    {
        SceneManager.activeSceneChanged += ChangedScene;

        gameObject.GetComponent<AudioSource>().volume = volume;

        switch (levelTracker)
        {
            case 0:
                gameObject.GetComponent<AudioSource>().clip = theme;
                gameObject.GetComponent<AudioSource>().Play();
                levelTracker = -1;
                break;
            case 1:
                gameObject.GetComponent<AudioSource>().clip = lvl1Theme;
                gameObject.GetComponent<AudioSource>().Play();
                levelTracker = -1;
                break;
            case 2:
                gameObject.GetComponent<AudioSource>().clip = lvl2Theme;
                gameObject.GetComponent<AudioSource>().Play();
                levelTracker = -1;
                break;
            case 3:
                gameObject.GetComponent<AudioSource>().clip = lvl2Theme;
                gameObject.GetComponent<AudioSource>().Play();
                levelTracker = -1;
                break;
        }

    }
    void ChangedScene(Scene current, Scene next)
    {
        switch (next.name)
        {
            case "MainMenu":
                levelTracker = 0;
                break;
            case "Level_1":
                levelTracker = 1;
                        break;
            case "Level_2":
                levelTracker = 2;
                break;
            case "Level_3":
                levelTracker = 3;
                break;
        }
    }
}
