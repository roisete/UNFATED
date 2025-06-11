using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

public class OpenChest : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private LocalizeStringEvent localizedStringEvent;
    [SerializeField]
    private LocalizeStringEvent alreadyOpenedStringEvent;
    [SerializeField]
    private GameObject audioSourceDialog;
    [SerializeField]
    private GameObject chestNotOpened;
    [SerializeField]
    private GameObject chestOpened;
    [SerializeField]
    private GameObject interactionIcon;
    public int chestID;
    [SerializeField]
    private string item;
    [SerializeField]
    private int itemQuantity;
    [SerializeField]
    private string itemType;

    private bool isGamePaused = false;
    private bool playerInRange = false;
    private bool hasBeenOpened = false;

    private void Start()
    {
        SpriteRenderer cO = chestOpened.GetComponent<SpriteRenderer>();
        cO.enabled = false;
        if (interactionIcon != null)
            interactionIcon.SetActive(false);
        if (dialogBox != null)
            dialogBox.SetActive(false);

        if (localizedStringEvent != null && textBox != null)
            localizedStringEvent.OnUpdateString.AddListener(UpdateDialogText);

        //Chequea se se abriu o cofre anteriormente
        hasBeenOpened = Items.instance.HasChestKey(chestID);
        if (hasBeenOpened)
            cO.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionIcon != null && !hasBeenOpened)
                interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionIcon != null)
                interactionIcon.SetActive(false);
            if (isGamePaused)
                HideDialog();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.C))
            if (!isGamePaused)
                ShowDialog();
            else
                HideDialog();
        else if (isGamePaused && Input.GetKeyDown(KeyCode.X))
            HideDialog();
    }

    private void ShowDialog()
    {
        if (!hasBeenOpened)
            FirstTimeOpen();
        else
            ShowAlreadyOpenedDialog();
    }

    private void FirstTimeOpen()
    {
        SpriteRenderer cN = chestNotOpened.GetComponent<SpriteRenderer>();
        SpriteRenderer cO = chestOpened.GetComponent<SpriteRenderer>();
        if (dialogBox != null && textBox != null)
        {
            dialogBox.SetActive(true);
            if (interactionIcon != null)
                interactionIcon.SetActive(false);

            localizedStringEvent.RefreshString();

            //Añade itemss
            for (int i = 0; i < itemQuantity; i++)
            {
                if (itemType == "Health")
                    Items.instance.healthItems.Add(item);
                else if (itemType == "KeyItem")
                    Items.instance.keyItems.Add(item);
            }

            //Marca o cofre como aberto
            Items.instance.AddChestKey(chestID);
            hasBeenOpened = true;
            cO.enabled = true;
            cN.enabled = false;
            Time.timeScale = 0;
            isGamePaused = true;
            PlayDialogSound();
        }
    }

    private void ShowAlreadyOpenedDialog()
    {
        if (dialogBox != null && textBox != null)
        {
            dialogBox.SetActive(true);
            alreadyOpenedStringEvent.OnUpdateString.AddListener(UpdateDialogText);
            alreadyOpenedStringEvent.RefreshString();
            alreadyOpenedStringEvent.OnUpdateString.RemoveListener(UpdateDialogText);
            
            Time.timeScale = 0;
            isGamePaused = true;
            PlayDialogSound();
        }
    }

    private void HideDialog()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
            Time.timeScale = 1;
            isGamePaused = false;
            PlayDialogSound();
        }
    }

    private void UpdateDialogText(string value)
    {
        textBox.GetComponent<Text>().text = value;
    }

    private void PlayDialogSound()
    {
        if (audioSourceDialog != null)
        {
            var source = audioSourceDialog.GetComponent<AudioSource>();
            if (source != null)
                source.Play();
        }
    }
}
