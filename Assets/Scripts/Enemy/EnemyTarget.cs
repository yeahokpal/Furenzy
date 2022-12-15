using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public int health;
    public float KnockbackPower = 100;
    public float KnockbackDuration = 1;

    public GameObject attacker;
    public Rigidbody2D rb;
    public static EnemyTarget instance;
    public AudioSource audioSource;
    public AudioClip Hit;
    public AIPath aipath;

    private bool i = false;

    private GameObject tracking;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
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
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(1);
            attacker = collision.gameObject;
            StartCoroutine(PlayerManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
        }

        if (collision.gameObject.name == "Lightning(Clone)")
        {
            GameObject.Find("Fox(Clone)").GetComponent<PlayerManager>().MP_Up();
        }
    }


    public void TakeDamage(int damage)
    {    
        health = health - damage;
        StartCoroutine(HitStunWait(.5f));
        gameObject.GetComponent<ParticleSystem>().Play();
        audioSource.Play();
        if (health <= 0)
        {
            StartCoroutine(WaitToDestroy());
        }
    }

    public IEnumerator HitStunWait(float StunTime)
    {
        aipath.maxSpeed = 0f;
        aipath.canMove = false;
        yield return new WaitForSeconds(StunTime);
        aipath.maxSpeed = 3.5f;
        aipath.canMove = true;
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
