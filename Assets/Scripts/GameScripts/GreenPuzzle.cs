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
    [SerializeField]
    private GameObject interactionIcon;


    // Start is called before the first frame update
    void Start()
    {
        interactionIcon?.SetActive(false);
        greenOrb?.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionIcon.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionIcon.SetActive(false);
            playerInRange = false;
        }
    }

    // OnTrigger only work the exact frame it enters, so we can´t make the condition there
    private void Update()
    {
        if (playerInRange && !isTriggered && Input.GetKeyDown(KeyCode.C))
        {
            lever.GetComponent<AudioSource>().Play();
            interactionIcon.SetActive(false);
            StartCoroutine(OrbActivation(greenOrb));
            isTriggered = true;
            this.gameObject.GetComponent<Collider2D>().enabled = false; //Deshabilita o colliders
        }
    }

    private IEnumerator OrbActivation(GameObject orb){
        yield return new WaitForSeconds(0.5f);
        orb.SetActive(true);
        audioSource.GetComponent<AudioSource>().Play();
    }
}
