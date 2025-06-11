using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    private static ShowMenu instance;
    public static ShowMenu Instance => instance;

    [Header("Menu References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource menuSound;

    [Header("Settings")]
    [SerializeField] private float inputCooldown = 0.2f;

    private bool isGamePaused;
    private float lastInputTime;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        HandlePauseInput();
    }

    private void HandlePauseInput()
    {
        if (!CanProcessInput()) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lastInputTime = Time.unscaledTime;
            TogglePauseState();
        }
    }

    private bool CanProcessInput()
    {
        return Time.unscaledTime - lastInputTime >= inputCooldown;
    }

    private void TogglePauseState()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

        PlayMenuSound();
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        Debug.Log("Game Paused");
    }

    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        Debug.Log("Game Resumed");
    }

    private void PlayMenuSound()
    {
        if (menuSound != null && menuSound.gameObject.activeInHierarchy)
        {
            menuSound.Play();
        }
    }

    public bool IsPaused() => isGamePaused;
}
