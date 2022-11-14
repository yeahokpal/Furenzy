using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTarget : MonoBehaviour
{
    bool i = false;
    GameObject tracking;
    public int health;
    public GameObject attacker;
    public float KnockbackPower = 100;

    public float KnockbackDuration = 1;
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
            {
                collision.gameObject.GetComponent<FoxManager>().TakeDamage(1);
                attacker = collision.gameObject;
            }     
            if (collision.gameObject.name == "Bunny(Clone)")
                collision.gameObject.GetComponent<BunnyManager>().TakeDamage(1);
            if (collision.gameObject.name == "Bird(Clone)")
                collision.gameObject.GetComponent<BirdManager>().TakeDamage(1);
            if (collision.gameObject.name == "Ferret(Clone)")
                collision.gameObject.GetComponent<FerretManager>().TakeDamage(1);
            attacker = collision.gameObject;
            StartCoroutine(FoxManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
        }
    }

    public void TakeDamage(int damage)
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        if (collision.gameObject.name == "Fox(Clone)")
            collision.gameObject.GetComponent<FoxManager>().MP_Up(); 
        if (collision.gameObject.name == "Bunny(Clone)")
            collision.gameObject.GetComponent<BunnyManager>().MP_Up();
        if (collision.gameObject.name == "Bird(Clone)")
            collision.gameObject.GetComponent<BirdManager>().MP_Up();
        if (collision.gameObject.name == "Ferret(Clone)")
            collision.gameObject.GetComponent<FerretManager>().MP_Up();
        health = health - damage;
        if (health <= 0)
        {
            StartCoroutine(WaitToDestroy());
        }
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
