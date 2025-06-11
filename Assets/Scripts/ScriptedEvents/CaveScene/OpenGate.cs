using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

public class OpenGate : MonoBehaviour
{
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private GameObject purpleOrb;
    [SerializeField]
    private GameObject lockedTrigger;
    [SerializeField]
    private GameObject unlockedSound;
    private bool playerInRange = false;

    //Variables diálogo
    [SerializeField]
    private GameObject note;
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private LocalizeStringEvent dialogLocalizeStringEvent;
    [SerializeField]
    private GameObject audioSource;
    [SerializeField]
    private GameObject interactionIcon;

    private bool isGamePaused = false;
    private bool hasDialogOpened = false;

    private string[] dialogParts;
    private int textIndex = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Items.instance.HasTagItem(purpleOrb))
        {
            lockedTrigger.SetActive(false);
        }
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            interactionIcon.SetActive(true);
            Debug.Log("Zona de interacción activada");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactionIcon.SetActive(false);
            Debug.Log("Chao chao");
        }
    }

    private void PauseGame()
    {
        dialogBox.SetActive(true);
        interactionIcon.SetActive(false);
        textBox.GetComponent<Text>().text = dialogParts[textIndex].Trim();
        Time.timeScale = 0;
        isGamePaused = true;
        hasDialogOpened = true;
        Debug.Log("Dialogo aberto");
    }

    private void ContinueGame()
    {
        dialogBox.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        textIndex = 0;
    }

    void Start()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
        if (dialogBox != null)
            dialogBox.SetActive(false);

        //Obtén o texto do diálogo e divídeo
        string dialogText = dialogLocalizeStringEvent.StringReference.GetLocalizedString();
        dialogParts = dialogText.Split(new string[] { "||" }, System.StringSplitOptions.None);
        textIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Abre porta e diálogo
        if (Input.GetKeyDown(KeyCode.C) && playerInRange && !isGamePaused && !hasDialogOpened && Items.instance.HasTagItem(purpleOrb))
        {
            StartCoroutine(OpeningGate());
            gate.SetActive(false);
            audioSource.GetComponent<AudioSource>().Play();
            PauseGame();
        }

        //Diálogo xa aberto
        if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.X))
            {
                audioSource.GetComponent<AudioSource>().Play();
                if (textIndex < dialogParts.Length)
                {
                    textBox.GetComponent<Text>().text = dialogParts[textIndex].Trim();
                    Debug.Log("Enseñando dialogo no index " + textIndex);
                    textIndex++;
                }
                else
                {
                    ContinueGame();
                    note.SetActive(false);
                }
            }
        }
    }

    private IEnumerator OpeningGate()
    {
        if (unlockedSound.GetComponent<AudioSource>() != null)
        {
            unlockedSound.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(unlockedSound.GetComponent<AudioSource>().clip.length);
            unlockedSound.SetActive(false);
        }
    }

    //Poderíase destruir o monitor para evitar seguir premendoo
}