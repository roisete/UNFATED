using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotinJrDialog : MonoBehaviour
{
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

    void Start()
    {
        dialogBox.SetActive(false);
    }

    private void PauseGame()
    {
        dialogBox.SetActive(true);
        textBox.GetComponent<Text>().text = textValue[textIndex];
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

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());

        // Open dialog
        if (!isGamePaused && !hasDialogOpened)
        {
            PauseGame();
        }

        // Dialog already opened
        if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                audioSource.GetComponent<AudioSource>().Play();
                // Showing more text
                if (textIndex < textValue.Count)
                {
                    textBox.GetComponent<Text>().text = textValue[textIndex];
                    textIndex++;
                    Debug.Log("Enseñando dialogo no index" + textIndex);
                }
                // Close dialog
                else
                {
                    ContinueGame();
                    Debug.Log("Diálogo pechado");
                    SceneManager.LoadScene("StartScene");
                }
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
