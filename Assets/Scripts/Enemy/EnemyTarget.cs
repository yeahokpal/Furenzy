using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    bool i = false;
    GameObject tracking;
    public int health;
    public GameObject attacker;
    public float KnockbackPower = 100;

    public static EnemyTarget instance;
    public Rigidbody2D rb;

    public float KnockbackDuration = 1;

    private bool AIPathExists;
    private AIPath aipath;

    private void Awake()
    {
        instance = this;

        AIPathExists = transform.TryGetComponent(out aipath);
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
            if (collision.gameObject.name == "Fox(Clone)")
            {
                collision.gameObject.GetComponent<FoxManager>().TakeDamage(1);
                attacker = collision.gameObject;
                StartCoroutine(FoxManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
            }     
            if (collision.gameObject.name == "Bunny(Clone)")
            {
                collision.gameObject.GetComponent<BunnyManager>().TakeDamage(1);
                attacker = collision.gameObject;
                StartCoroutine(BunnyManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
            }
                
            if (collision.gameObject.name == "Bird(Clone)")
            {
                collision.gameObject.GetComponent<BirdManager>().TakeDamage(1);
                attacker = collision.gameObject;
                StartCoroutine(BirdManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
            }
                
            if (collision.gameObject.name == "Ferret(Clone)")
            {
                collision.gameObject.GetComponent<FerretManager>().TakeDamage(1);
                attacker = collision.gameObject;
                StartCoroutine(FerretManager.instance.Knockback(KnockbackDuration, KnockbackPower, this.transform));
            }
        }

        if (collision.gameObject.name == "Lightning(Clone)")
        {
            GameObject.Find("Fox(Clone)").GetComponent<FoxManager>().MP_Up();
        }
    }


    public void TakeDamage(int damage)
    {    
        health = health - damage;
        gameObject.GetComponent<ParticleSystem>().Play();
        if (health <= 0)
        {
            StartCoroutine(WaitToDestroy());
        }
    }

    public IEnumerator HitStunWait(float StunTime)
    {
        Debug.Log("HitStunWait");
        yield return new WaitForSeconds(StunTime);
        aipath.maxSpeed = 3.5f;
        Debug.Log("HitStunEnd");
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
