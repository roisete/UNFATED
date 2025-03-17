using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    private bool isGamePaused;
    private bool canInteract = true;

    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        audioSource.Play();
        Time.timeScale = 0;
        isGamePaused = true;

    }
    private void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Track key releases to prevent multiple actions when pressing once
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            canInteract = true;
        }

        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                audioSource.Play();
                canInteract = false;
                if (isGamePaused)
                {
                    ContinueGame();
                    Debug.Log("Xogo en marcha");
                }
                else
                {
                    PauseGame();
                    Debug.Log("Xogo en pausa");
                }
            }
        }
    }
}
