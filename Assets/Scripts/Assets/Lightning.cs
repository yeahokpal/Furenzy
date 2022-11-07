using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public int damage = 2;
    public int speed = 20;
    bool canCollide = false;
    bool canCircle = false;
    public Animator anim;
    public CircleCollider2D CircleCol;
    private void Awake() // Setting Force Direction When it enters the scene
    {
        StartCoroutine(WaitToCollide());
        if (gameObject.transform.rotation == Quaternion.Euler(0, 0, 180))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        else if (gameObject.transform.rotation == Quaternion.Euler(0, 0, 90))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        else if (gameObject.transform.rotation == Quaternion.Euler(0, 0, 0))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed, ForceMode2D.Impulse);
        else if (gameObject.transform.rotation == Quaternion.Euler(0, 0, -90))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        // Starts coroutine to k*ll itself
        StartCoroutine(WaitToDestroy());
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        anim.SetBool("CanCollide", true);
    }
    private void OnCollisionEnter2D(Collision2D collision) // If it hits an enemy, deal damage
    {
        if (canCollide)
        {
            if (collision.transform.tag == "Enemy")
            {
                Debug.Log("Lmao11");
                collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
                canCollide = false;
                canCircle = true;
                StartCoroutine(WaitToDestroy(0.2f));
                CircleCol.enabled = true;
            }
            else
            {
                StartCoroutine(WaitToDestroy(0f));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // canCircle is confused when multiple enemies can be hit, becomes false after just 1 hit
        if (collision.tag == "Enemy" && canCircle)
        {
            Debug.Log("Lmao11");
            anim.SetBool("CanCollide", false);
            collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
            canCircle = false;
        }
    }

    IEnumerator WaitToDestroy() // Destroy itself after 2 seconds
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator WaitToDestroy(float time) // Destroy itself after 2 seconds
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    IEnumerator WaitToCollide()
    {
        yield return new WaitForSeconds(.05f);
        canCollide = !canCollide;
    }

    private void Update()
    {
        if (anim.GetBool("CanCollide") == false)
        {
            anim.Play("LightningCircle");
        }
    }
}
