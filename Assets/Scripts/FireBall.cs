using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 1;
    public int speed = 5;

    private void Awake()
    {
        if (gameObject.transform.rotation == Quaternion.Euler(0,0,180))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        else if (gameObject.transform.rotation == Quaternion.Euler(0, 0, 90))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        else if (gameObject.transform.rotation == Quaternion.Euler(0, 0, 0))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed, ForceMode2D.Impulse);
        else if (gameObject.transform.rotation == Quaternion.Euler(0, 0, -90))
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        StartCoroutine(WaitToDestroy());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Character")
        {
            collision.gameObject.GetComponent<FoxManager>().TakeDamage(damage);
            collision.gameObject.GetComponent<BunnyManager>().TakeDamage(damage);
            collision.gameObject.GetComponent<BirdManager>().TakeDamage(damage);
            // Not ready yet lol    collision.gameObject.GetComponent<FerretManager>().TakeDamage(damage);
        }
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
