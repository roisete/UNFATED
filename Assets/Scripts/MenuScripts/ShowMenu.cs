using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu = new();
    bool isGamePaused;

    [SerializeField]
    private AudioSource audioSource = new();

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    
    }
    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Cancel") == 1 || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                audioSource.Play();
                ContinueGame();
                Debug.Log("Game should NOT be paused rn");
            } 
            else
            {
                audioSource.Play();
                PauseGame();
                Debug.Log("Game should be paused rn");
            }
        }
    }
}
