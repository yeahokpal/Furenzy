using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControlScript : MonoBehaviour
{
    public static MusicControlScript instannce;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(instannce == null)
        {
            instannce = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
