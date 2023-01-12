/*
 * Programmer: Jack
 * Purpose: Being a univeral script for interactable objects
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public bool heal;
    public bool die;
    bool i = true;
    public TextMeshProUGUI UIToIncrease = null;
    public TextMeshProUGUI UIToRead = null;
    public Sprite newSprite;
    public GameObject check1, check2, check3, spawnThis;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (heal)
        {
            switch (collision.name)
            {
                case "Fox(Clone)":
                    if (collision.GetComponent<PlayerManager>().Health < 3)
                    {
                        collision.GetComponent<PlayerManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;

                case "Bunny(Clone)":
                    if (collision.GetComponent<PlayerManager>().Health < 3)
                    {
                        collision.GetComponent<PlayerManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;

                case "Bird(Clone)":
                    if (collision.GetComponent<PlayerManager>().Health < 3)
                    {
                        collision.GetComponent<PlayerManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;

                case "Ferret(Clone)":
                    if (collision.GetComponent<PlayerManager>().Health < 3)
                    {
                        collision.GetComponent<PlayerManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;
            }
        }

        if (die)
            Destroy(gameObject);

        if (UIToIncrease != null)
        {
            int num = int.Parse(UIToIncrease.text);
            ++num;
            UIToIncrease.text = num.ToString();
        }

        if (newSprite != null)
        {
            if (UIToRead != null)
            {
                if (int.Parse(UIToRead.text) > 0)
                {
                    UIToRead.text = (int.Parse(UIToRead.text) - 1).ToString();
                    gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
                }
            }
        }
    }
    private void Update()
    {
        if ((check1 != null && check2 != null && check3 != null) && (i))
        {
            if ((check1.GetComponent<SpriteRenderer>().sprite.name == "KeyholeFilled") && (check2.GetComponent<SpriteRenderer>().sprite.name == "KeyholeFilled") && (check3.GetComponent<SpriteRenderer>().sprite.name == "KeyholeFilled"))
            {
                Instantiate(spawnThis, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                i = false;
            }
        }
    }
}