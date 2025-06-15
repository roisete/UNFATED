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
    private AudioSource trunkSound;
    [SerializeField]
    private GameObject miniTrunk;
    private bool isPuzzleSolved = false;
    [SerializeField]
    private Animator squirrel;


    // Start is called before the first frame update
    void Start()
    {

    }

    //Reseta puzzle
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
        if ((CheckPuzzle() && !isPuzzleSolved) || CharacterEvents.instance.puzzleResolved)
        {
            StartCoroutine(OrbActivation(redOrb));
            isPuzzleSolved = true;
        }else if (!CheckPuzzle())
        {
            trunk.SetActive(true);
            miniTrunk.SetActive(true);
            isPuzzleSolved = false;
        }
    }

    private IEnumerator OrbActivation(GameObject orb)
    {
        trunk.SetActive(false);
        miniTrunk.SetActive(false);
        squirrel.SetTrigger("Contact");
        trunkSound.Play();
        CharacterEvents.instance.puzzleResolved = true;
        yield return new WaitForSeconds(trunkSound.clip.length);
        orb.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(orb.GetComponent<AudioSource>().clip.length);
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
