using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

public class LongDialogAppear : MonoBehaviour
{
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

    private bool playerInRange = false;
    private bool isGamePaused = false;
    private bool hasDialogOpened = false;

    private string[] dialogParts;
    private int textIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        Debug.Log("Diálogo pechado");
    }

    void Update()
    {
        //Abre diálogo
        if (Input.GetKeyDown(KeyCode.C) && playerInRange && !isGamePaused && !hasDialogOpened)
        {
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
}
