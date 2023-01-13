/*
 * Programmer: Caden
 * Purpose: Enemy tracking, damage, hitstun & collisions
 * Input: Player attacks and position
 * Output: Enemy damage, audio, knockback
 */

using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    #region Variables
    //Stats
    public int health;

    //Hitstun and damage
    public float KnockbackPower = 100;
    public float KnockbackDuration = 1;
    public GameObject attacker;
    public static EnemyTarget instance;

    //Audio
    public AudioClip Hit;
    public AudioSource audioSource;

    //Tracking
    public Rigidbody2D rb;
    public AIPath aipath;
    private GameObject tracking;
    private bool i = false;

    #endregion

    #region Default Methods
    private void Awake()
    {
        aipath = gameObject.GetComponent<AIPath>();
        instance = this; //used for hitstun
    }
    // Update is called once per frame
    void Update()
    {
        //checks to see if enemy is tracking a player
        if (tracking != null)
            gameObject.GetComponent<AIDestinationSetter>().target = tracking.transform;
        if (gameObject.GetComponent<AIDestinationSetter>().target == null)
        {
            i = false;
        }
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
        //deal knockback to player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(1);
            attacker = collision.gameObject;
            StartCoroutine(PlayerManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
        }

        //refill player mana on hit
        if (collision.gameObject.name == "Lightning(Clone)")
        {
            GameObject.Find("Fox(Clone)").GetComponent<PlayerManager>().MP_Up();
        }

        if (collision.gameObject.name == "Arrow(Clone)")
        {
            GameObject.Find("Bird(Clone)").GetComponent<PlayerManager>().MP_Up();
        }
    }

    #endregion

    #region Custom Methods

    public void TakeDamage(int damage)//enemy takes damage
    {    
        health = health - damage;
        StartCoroutine(HitStunWait(.4f));
        gameObject.GetComponent<ParticleSystem>().Play();
        audioSource.Play();
        if (health <= 0)
        {
            StartCoroutine(WaitToDestroy());
        }
    }

    public IEnumerator HitStunWait(float StunTime)//creates a delay between when player hits and when enemy can move again
    {
        aipath.maxSpeed = 0f;
        aipath.canMove = false;
        yield return new WaitForSeconds(StunTime);
        aipath.maxSpeed = 3.5f;
        aipath.canMove = true;
    }

    IEnumerator WaitToDestroy()//destroys enemy on death
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    #endregion
}
