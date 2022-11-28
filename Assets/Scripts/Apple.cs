using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Fox(Clone)")
        {
            collision.GetComponent<FoxManager>().Health++;
            Destroy(gameObject); //*dies
        }

        if (collision.name == "Bunny(Clone)")
        {
            collision.GetComponent<BunnyManager>().Health++;
            Destroy(gameObject); //*dies
        }

        if (collision.name == "Ferret(Clone)")
        {
            collision.GetComponent<FerretManager>().Health++;
            Destroy(gameObject); //*dies
        }

        if (collision.name == "Bird(Clone)")
        {
            collision.GetComponent<BirdManager>().Health++;
            Destroy(gameObject); //*dies
        }
    }
}
