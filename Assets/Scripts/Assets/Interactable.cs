using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public bool heal;
    public TextMeshProUGUI UI = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (heal)
        {
            switch (collision.name)
            {
                case "Fox(Clone)":
                    if (collision.GetComponent<FoxManager>().Health < 3)
                    {
                        collision.GetComponent<FoxManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;

                case "Bunny(Clone)":
                    if (collision.GetComponent<BunnyManager>().Health < 3)
                    {
                        collision.GetComponent<BunnyManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;

                case "Bird(Clone)":
                    if (collision.GetComponent<BirdManager>().Health < 3)
                    {
                        collision.GetComponent<BirdManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;

                case "Ferret(Clone)":
                    if (collision.GetComponent<FerretManager>().Health < 3)
                    {
                        collision.GetComponent<FerretManager>().Health++;
                        Destroy(gameObject);
                    }
                    break;
            }
        }

        if (UI != null)
        {
            int num = int.Parse(UI.text);
            ++num;
            UI.text = num.ToString();
        }
    }
}