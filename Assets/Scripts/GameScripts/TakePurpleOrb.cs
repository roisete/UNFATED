using System.Collections;
using UnityEngine;

public class TakePurpleOrb : MonoBehaviour
{
    [SerializeField]
    private GameObject orb;
    [SerializeField]
    private GameObject note;
    private bool playerInRange = false;

    private void Start()
    {
        if (CharacterEvents.instance.purpleOrb)
        {
            gameObject.SetActive(false);
            note.SetActive(false);
            orb.SetActive(false);
        }
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

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !CharacterEvents.instance.purpleOrb && Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(TakingOrb());
            orb.SetActive(false);
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
        CharacterEvents.instance.purpleOrb = true;
    }
}
