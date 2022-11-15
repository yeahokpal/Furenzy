using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 3;
    public int speed = 15;
    bool canCollide = false;

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
        // Starts coroutine to kill itself
        StartCoroutine(WaitToDestroy());
        StartCoroutine(WaitToCollide());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollide)
        {
            if (collision.transform.tag == "Enemy" && collision is BoxCollider2D)
            {
                collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
                StartCoroutine(collision.gameObject.GetComponent<EnemyTarget>().OnHit());
            }
        }
    }

    IEnumerator WaitToDestroy() // Destroy itself after 2 seconds
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    IEnumerator WaitToCollide()
    {
        yield return new WaitForSeconds(.05f);
        canCollide = true;
    }
}
