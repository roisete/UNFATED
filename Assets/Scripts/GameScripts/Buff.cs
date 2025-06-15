using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private LocalizeStringEvent localizedStringEvent;
    [SerializeField]
    private GameObject audioSource;
    [SerializeField]
    private GameObject interactionIcon;
    [SerializeField]
    private string itemToDestroy;

    private bool isGamePaused = false;
    private bool playerInRange = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            interactionIcon.SetActive(true);
            Debug.Log("En rango");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactionIcon.SetActive(false);
            if (isGamePaused)
            {
                HideDialog();
            }
        }
    }

    void Start()
    {
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
        if (dialogBox != null)
            dialogBox.SetActive(false);

        // Configurar o texto do diálogo inicial
        if (localizedStringEvent != null && textBox != null)
        {
            localizedStringEvent.OnUpdateString.AddListener(UpdateDialogText);
        }
        if (NotesRead.instance.isRead(itemToDestroy))
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateDialogText(string value)
    {
        textBox.GetComponent<Text>().text = value;
    }

    private void ShowDialog()
    {
        if (dialogBox != null && textBox != null)
        {
            dialogBox.SetActive(true);
            interactionIcon.SetActive(false);
            //Forzar a actualización do texto do diálogo
            localizedStringEvent.RefreshString();
            Time.timeScale = 0;
            isGamePaused = true;
            PlayAudio();
            Debug.Log("Dialog shown");
        }
    }

    private void HideDialog()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
            Time.timeScale = 1;
            isGamePaused = false;
            PlayAudio();
            Debug.Log("Dialog hidden");
            //Expandir para facer dinámico
            CharacterStats.instance.attack += 30;
            CharacterStats.instance.defense += 30;
            Invoke("Exit", 1.5f);
        }
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            var source = audioSource.GetComponent<AudioSource>();
            if (source != null)
                source.Play();
        }
    }

    void Update()
    {
        if (playerInRange && !isGamePaused && Input.GetKeyDown(KeyCode.C))
        {
            ShowDialog();
        }
        else if (isGamePaused && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C)))
        {
            HideDialog();
        }
    }

    void Exit()
    {
        gameObject.SetActive(false);
        NotesRead.instance.notes.Add(itemToDestroy);
    }
}
