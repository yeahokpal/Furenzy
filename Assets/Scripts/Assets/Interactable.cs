using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public bool heal;
    public TextMesh UI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (heal)
        {
            switch (collision.name)
            {
                case "Fox(Clone)":
                    collision.GetComponent<FoxManager>().Health++;
                    Destroy(gameObject);
                    break;

                case "Bunny(Clone)":
                    collision.GetComponent<BunnyManager>().Health++;
                    Destroy(gameObject);
                    break;

                case "Bird(Clone)":
                    collision.GetComponent<BirdManager>().Health++;
                    Destroy(gameObject);
                    break;

                case "Ferret(Clone)":
                    collision.GetComponent<FerretManager>().Health++;
                    Destroy(gameObject);
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