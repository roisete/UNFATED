using UnityEngine;
using System.Collections.Generic;

public class END : MonoBehaviour
{
    [Header ("Animation Settings")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource sound;

    private void Bye()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
        SceneSpawnManager.instance.ChangeScene("IntroStart");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sound.Play();
            anim.SetTrigger("Start");
            Invoke("Bye", 2.5f);
        }
    }
}
