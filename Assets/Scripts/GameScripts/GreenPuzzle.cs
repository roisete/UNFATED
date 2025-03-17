using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPuzzle : MonoBehaviour
{

    [SerializeField]
    private GameObject lever;
    private bool isTriggered = false;

    [SerializeField]
    private GameObject greenOrb;

    [SerializeField]
    private GameObject audioSource;

    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        greenOrb.SetActive(false);
    }

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

    // OnTrigger only work the exact frame it enters, so we can´t make the condition there
    private void Update()
    {
        if (playerInRange && !isTriggered && Input.GetKeyDown(KeyCode.C))
        {
            lever.GetComponent<AudioSource>().Play();
            StartCoroutine(OrbActivation(greenOrb));
            isTriggered = true;
        }
    }

    private IEnumerator OrbActivation(GameObject orb){
        yield return new WaitForSeconds(0.5f);
        orb.SetActive(true);
        audioSource.GetComponent<AudioSource>().Play();
    }
}
