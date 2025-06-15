using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rest : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSource.Play();
            CharacterStats.instance.health = CharacterStats.instance.maxHealth;
            CharacterStats.instance.mana = CharacterStats.instance.maxMana;
        }
    }
}
