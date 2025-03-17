using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeNote : MonoBehaviour
{
[SerializeField]
    private GameObject note;

    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    private int textIndex = 0;
    [SerializeField]
    private List<string> textValue;
    [SerializeField]
    private GameObject audioSource;

    private bool playerInRange = false;
    private bool isGamePaused = false;
    private bool hasDialogOpened = false; // Ensure dialog only opens once

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("Player entered trigger zone");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

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

    // Update is called once per frame
    void Update()
    {
        // Open dialog
        if (Input.GetKeyDown(KeyCode.C) && playerInRange && !isGamePaused && !hasDialogOpened)
        {
            audioSource.GetComponent<AudioSource>().Play();
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
                    note.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                audioSource.GetComponent<AudioSource>().Play();
                ContinueGame();
            }
        }
    }
}

