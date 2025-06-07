using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class RobotinJrDialog : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private LocalizeStringEvent dialogLocalizeStringEvent;
    [SerializeField]
    private GameObject audioSource;

    private bool isGamePaused = false;
    private bool hasDialogOpened = false;

    private string[] dialogParts;
    private int textIndex = 0;

    void Start()
    {
        dialogBox.SetActive(false);

        //Obtén o texto do diálogo e divídeo en partes
        string dialogText = dialogLocalizeStringEvent.StringReference.GetLocalizedString();
        dialogParts = dialogText.Split(new string[] { "||" }, System.StringSplitOptions.None);
        textIndex = 0;
    }

    private void PauseGame()
    {
        dialogBox.SetActive(true);
        textBox.GetComponent<Text>().text = dialogParts[textIndex].Trim();
        Time.timeScale = 0;
        isGamePaused = true;
        hasDialogOpened = true;
    }

    private void ContinueGame()
    {
        dialogBox.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        textIndex = 0;
    }

    void Update()
    {
        //Abre diálogo
        if (!isGamePaused && !hasDialogOpened)
        {
            PauseGame();
        }

        //Diálogo xa aberto
        if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                audioSource.GetComponent<AudioSource>().Play();
                textIndex++;
                //Mostrando máis texto
                if (textIndex < dialogParts.Length)
                {
                    textBox.GetComponent<Text>().text = dialogParts[textIndex].Trim();
                    Debug.Log("Enseñando dialogo no index " + textIndex);
                }
                //Pecha diálogo
                else
                {
                    ContinueGame();
                    Debug.Log("Diálogo pechado");
                    SceneManager.LoadScene("StartScene");
                }
            }
        }
    }
}
