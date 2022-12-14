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
    public TextMeshProUGUI UIToIncrease = null;
    public TextMeshProUGUI UIToRead = null;
    public Sprite newSprite;

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
            else { gameObject.GetComponent<SpriteRenderer>().sprite = newSprite; }
        }
    }
}