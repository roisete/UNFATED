using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> runes;
    [SerializeField]
    private GameObject redOrb;
    [SerializeField]
    private GameObject trunk;
    [SerializeField]
    private GameObject miniTrunk;
    private bool isPuzzleSolved = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Acts like a reset button
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Esta tocando");
            foreach (var rune in runes)
            {
                rune.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isPuzzleSolved && CheckPuzzle())
        {
            StartCoroutine(OrbActivation(redOrb));
            trunk.SetActive(false);
            miniTrunk.SetActive(false);
            isPuzzleSolved = true;
        }else if (!CheckPuzzle())
        {
            trunk.SetActive(true);
            miniTrunk.SetActive(true);
            isPuzzleSolved = false;
        }
    }

    private IEnumerator OrbActivation(GameObject orb){
        trunk.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(trunk.GetComponent<AudioSource>().clip.length);
        orb.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(orb.GetComponent<AudioSource>().clip.length);
        trunk.SetActive(false);
        miniTrunk.SetActive(false);
    }

    private bool CheckPuzzle(){
        foreach (var rune in runes)
        {
            if (rune.GetComponent<SpriteRenderer>().color != Color.green)
            {
                return false;
            }
        }
        return true;
    }
}
