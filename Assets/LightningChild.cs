using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChild : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WaitToDestroy());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
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
