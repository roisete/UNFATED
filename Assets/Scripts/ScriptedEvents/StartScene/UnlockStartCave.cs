using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

public class UnlockStartCave : MonoBehaviour
{
    [SerializeField]
    private GameObject caveSound;

    [SerializeField]
    private List<GameObject> orbs = new();

    private bool caveOpened = false;

    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private LocalizeStringEvent dialogLocalizeStringEvent;
    [SerializeField]
    private GameObject audioSource;
    [SerializeField]
    private GameObject enterCave;
    private string[] dialogParts;
    private int textIndex = 0;
    private bool dialogActive = false;


    void Start()
    {
        caveSound.SetActive(false);
        dialogBox.SetActive(false);
        enterCave.SetActive(false);
        string dialogText = dialogLocalizeStringEvent.StringReference.GetLocalizedString();
        dialogParts = dialogText.Split(new string[] { "||" }, System.StringSplitOptions.None);
        textIndex = 0;
    }

    void Update()
    {
        ThreeOrbs();

        if (caveOpened && !CharacterEvents.instance.caveOpenedTriggered)
        {
            caveSound.SetActive(true);
            caveSound.GetComponent<AudioSource>().Play();
            DeactivateOrbs();
            CharacterEvents.instance.caveOpenedTriggered = true;
            enterCave.SetActive(true);
            //Mostrar diálogo 
            ShowDialog();
        }

        if (CharacterEvents.instance.caveOpenedTriggered)
        {
            enterCave.SetActive(true);
            caveSound.SetActive(true);
            DeactivateOrbs();
        }

        //Control do diálogo
        if (dialogActive)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                audioSource.GetComponent<AudioSource>().Play();
                textIndex++;
                if (textIndex < dialogParts.Length)
                {
                    textBox.GetComponent<Text>().text = dialogParts[textIndex].Trim();
                }
                else
                {
                    HideDialog();
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                audioSource.GetComponent<AudioSource>().Play();
                HideDialog();
            }
        }
    }

    private void ThreeOrbs()
    {
        if (Items.instance.keyItems.Contains("Green Orb") &&
            Items.instance.keyItems.Contains("Red Orb") &&
            Items.instance.keyItems.Contains("Blue Orb"))
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

    private void ShowDialog()
    {
        dialogBox.SetActive(true);
        textIndex = 0;
        textBox.GetComponent<Text>().text = dialogParts[textIndex].Trim();
        Time.timeScale = 0;
        dialogActive = true;
    }

    private void HideDialog()
    {
        dialogBox.SetActive(false);
        Time.timeScale = 1;
        dialogActive = false;
        textIndex = 0;
    }
}