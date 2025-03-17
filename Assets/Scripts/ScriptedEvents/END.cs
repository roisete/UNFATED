using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class END : MonoBehaviour
{
    public GameObject player = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            CharacterStats.instance.ChangeScene("Intro");
        }
    }
}
