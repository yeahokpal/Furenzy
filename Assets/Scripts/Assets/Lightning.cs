using System.Collections;
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
    private void OnCollisionEnter2D(Collision2D collision) // If it hits an enemy, deal damage
    {
        if (canCollide)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (collision.transform.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
            }
            Destroy(gameObject);
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
        canCollide = !canCollide;
    }
}
