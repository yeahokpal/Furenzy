using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public int damage = 1;
    public int speed = 20;
    bool canCollide = false;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canCollide)
        {
            if (collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
                canCollide = false;
                StartCoroutine(WaitToDestroy(0.2f));
            }
            else if (collision.transform.tag == "Wall")
            {
                StartCoroutine(WaitToDestroy(0f));
            }
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
}
