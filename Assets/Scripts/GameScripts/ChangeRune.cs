using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRune : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.GetComponent<SpriteRenderer>().color == Color.white)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (collision.gameObject.tag == "Player" && gameObject.GetComponent<SpriteRenderer>().color == Color.green)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
