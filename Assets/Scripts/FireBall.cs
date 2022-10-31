using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 1;
    public int speed = 5;

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
    private void OnCollisionEnter2D(Collision2D collision) // If it hits an enemy, deal damage
    {
        if (collision.transform.tag == "Enemy")
        {
            // Have damageing reference later
        }
    }
    IEnumerator WaitToDestroy() // Destroy itself after 2 seconds
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
