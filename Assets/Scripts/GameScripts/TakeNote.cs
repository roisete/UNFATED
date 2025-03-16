using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeNote : MonoBehaviour
{
    [SerializeField]
    private GameObject note = new GameObject();

    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    private int textIndex = 0;
    [SerializeField]
    private List<string> textValue;
    [SerializeField]
    private AudioSource audioSource;

    private bool isTriggered = false;
    private bool playerInRange = false;
    private bool isGamePaused = false;
    private bool canInteract = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
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
        // Track key releases to prevent multiple actions when pressing once
        if (Input.GetKeyUp(KeyCode.C))
        {
            canInteract = true;
        }

        // Open dialog
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.C) && playerInRange && !isTriggered && !isGamePaused)
            {
                canInteract = false; // Mark key as pressed until released
                audioSource.Play();
                isTriggered = true;
                PauseGame();
                Debug.Log("Dialogo aberto");
            }

            // Dialog already opened
            else if (isTriggered && isGamePaused)
            {
                canInteract = false;
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
                    isTriggered = false;
                    Debug.Log("Diálogo pechado");
                }
            }
        }
    }
}

