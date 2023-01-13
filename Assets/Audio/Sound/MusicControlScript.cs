/*
 * Programmer: Sliman
 * Purpose: Manages music settings and keeps the audio to be as the player sets
 * Input: Sound Settings
 * Output: Audio Output
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlScript : MonoBehaviour
{
    public static MusicControlScript instannce;

    public float volume;

    public void Awake()
    {

                DontDestroyOnLoad(this.gameObject);

        if (instannce == this)
        {
            instannce = null;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        if (GameObject.Find("Slider"))
        {
            volume = GameObject.Find("Slider").GetComponent<Slider>().value;
        }

        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.volume = volume;
        }
    }
}
