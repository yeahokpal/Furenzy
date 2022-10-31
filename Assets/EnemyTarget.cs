using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTarget : MonoBehaviour
{
    bool i = false;
    GameObject tracking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking != null)
            gameObject.GetComponent<AIDestinationSetter>().target = tracking.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && i == false)
        {
            tracking = collision.gameObject;
            i = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "Fox(Clone)")
                collision.gameObject.GetComponent<FoxManager>().TakeDamage(1);
            if (collision.gameObject.name == "Bunny(Clone)")
                collision.gameObject.GetComponent<BunnyManager>().TakeDamage(1);
            if (collision.gameObject.name == "Bird(Clone)")
                collision.gameObject.GetComponent<BirdManager>().TakeDamage(1);
            if (collision.gameObject.name == "Ferret(Clone)")
                collision.gameObject.GetComponent<FerretManager>().TakeDamage(1);
        }
    }
}
