/*
 * Programmer: Jack
 * Purpose: Be a universal controller for projectiles
 * Input: Player inputs
 * Output: Player actions
 */

using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage, speed;
    public bool trigger, collide;

    private void Awake() // Setting Force Direction When it enters the scene
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collide)
        {
            if (collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
                GameObject.Find("Fox(Clone)").GetComponent<PlayerManager>().MP_Up();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger)
        {
            if (collision.transform.tag == "Enemy" && collision is BoxCollider2D)
            {
                collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
            }
        }
    }

    IEnumerator WaitToDestroy() // Destroy itself after 2 seconds
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
