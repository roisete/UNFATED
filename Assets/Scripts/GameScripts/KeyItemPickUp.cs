using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

public class KeyItemPickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject keyItem;
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private LocalizeStringEvent localizedStringEvent;
    [SerializeField]
    private GameObject audioSourceDialog;
    [SerializeField]
    private GameObject interactionIcon;

    private bool isGamePaused = false;
    private bool playerInRange = false;
    private bool hasBeenTaken = false;

    private void Start()
    {
        //Chequea se o item foi pillado
        hasBeenTaken = Items.instance.HasTagItem(keyItem);

        if (hasBeenTaken)
        {
            gameObject.SetActive(false);
            keyItem.SetActive(false);
        }
        else
        {
            if (interactionIcon != null)
                interactionIcon.SetActive(false);
            if (dialogBox != null)
                dialogBox.SetActive(false);

            if (localizedStringEvent != null && textBox != null)
            {
                localizedStringEvent.OnUpdateString.AddListener(UpdateDialogText);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionIcon != null && !hasBeenTaken)
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
            {
                HideDialog();
            }
        }
    }

    private void Update()
    {
        if (playerInRange && !hasBeenTaken && Input.GetKeyDown(KeyCode.C))
        {
            if (!isGamePaused)
            {
                ShowDialog();
            }
            else
            {
                BeforeTakingItem();
            }
        }
        else if (isGamePaused && Input.GetKeyDown(KeyCode.X))
        {
            BeforeTakingItem();
        }
    }

    private void ShowDialog()
    {
        if (dialogBox != null && textBox != null)
        {
            dialogBox.SetActive(true);
            if (interactionIcon != null)
                interactionIcon.SetActive(false);

            localizedStringEvent.RefreshString();

            Time.timeScale = 0;
            isGamePaused = true;
            PlayDialogSound();
        }
    }

    //Antes de pillar o item quitamos diálogo
    private void BeforeTakingItem()
    {
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
            Time.timeScale = 1;
            isGamePaused = false;
            PlayDialogSound();

            TakeKeyItem();
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

    private void TakeKeyItem()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        AddItemtoInventory();
        
        //Desactivar todo
        hasBeenTaken = true;
        keyItem.SetActive(false);
        
        if (interactionIcon != null)
            interactionIcon.SetActive(false);

        gameObject.SetActive(false);
    }

    private void AddItemtoInventory()
    {
        Items.instance.keyItems.Add(keyItem.tag);
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
