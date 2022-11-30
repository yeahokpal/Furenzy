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
            if (collision.GetComponent<FoxManager>().Health <= 2)
            {
                collision.GetComponent<FoxManager>().Health++;
                Destroy(gameObject); //*dies
            }
        }

        if (collision.name == "Bunny(Clone)")
        {
            if (collision.GetComponent<BunnyManager>().Health <= 2)
            {
                collision.GetComponent<BunnyManager>().Health++;
                Destroy(gameObject); //*dies
            }
        }

        if (collision.name == "Ferret(Clone)")
        {
            if (collision.GetComponent<FerretManager>().Health <= 2)
            {
                collision.GetComponent<FerretManager>().Health++;
                Destroy(gameObject); //*dies
            }
        }

        if (collision.name == "Bird(Clone)")
        {
            if (collision.GetComponent<BirdManager>().Health <= 2)
            {
                collision.GetComponent<BirdManager>().Health++;
                Destroy(gameObject); //*dies
            }
        }
    }
}
