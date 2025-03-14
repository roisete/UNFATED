using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPuzzle : MonoBehaviour
{

    [SerializeField]
    private GameObject lever = new();
    private bool isTriggered = false;

    [SerializeField]
    private GameObject greenOrb = new();

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
    public void Update()
    {
        if (playerInRange && !isTriggered && Input.GetKeyDown(KeyCode.C))
        {
            lever.GetComponent<AudioSource>().Play();
            StartCoroutine(OrbActivation(greenOrb));
            isTriggered = true;
        }
    }

    public IEnumerator OrbActivation(GameObject orb){
        yield return new WaitForSeconds(0.5f);
        orb.SetActive(true);
        orb.GetComponent<AudioSource>().Play();
    }
}
