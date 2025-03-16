using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrb : MonoBehaviour
{
    [SerializeField]
    private GameObject orb = new GameObject();

    private bool isTriggered = false;
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !isTriggered && Input.GetKeyDown(KeyCode.C))
        {

            StartCoroutine(TakingOrb());
            orb.SetActive(false);
            isTriggered = true;
        }
    }

    private IEnumerator TakingOrb()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(gameObject.GetComponent<AudioSource>().clip.length);
        addOrbtoInventory(orb);
    }

    private void addOrbtoInventory(GameObject orb)
    {
        CharacterStats.instance.keyItems.Add(orb.tag);
    }
}
