using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterCave : MonoBehaviour
{

    [SerializeField]
    private GameObject cave;

    //Deactivate the orbs
    [SerializeField]
    private List<GameObject> orbs = new();

    private bool caveOpened = false;
    // Dialog elements
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    private int textIndex = 0;
    [SerializeField]
    private List<string> textValue;
    [SerializeField]
    private GameObject audioSource;
    private bool isGamePaused = false;
    private bool hasDialogOpened = false;


    // Start is called before the first frame update
    void Start()
    {
        cave.SetActive(false);
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ThreeOrbs();
        if (caveOpened && !CharacterEvents.instance.caveOpenedTriggered){
            cave.SetActive(true);
            cave.GetComponent<AudioSource>().Play();
            DeactivateOrbs();
            CharacterEvents.instance.caveOpenedTriggered = true;
        }
        if (CharacterEvents.instance.caveOpenedTriggered)
        {
            cave.SetActive(true);
            DeactivateOrbs();
        }
    }

    private void ThreeOrbs(){
        if (CharacterStats.instance.keyItems.Contains("Green Orb") && CharacterStats.instance.keyItems.Contains("Red Orb") && CharacterStats.instance.keyItems.Contains("Blue Orb"))
        {
            Debug.Log("3 orbes");
            caveOpened = true;
        }
    }

    private void DeactivateOrbs()
    {
        foreach (var orb in orbs)
        {
            orb.SetActive(false);
        }
    }
}
