using UnityEngine;
using UnityEngine.UI;

public class StandardDialogAppear : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private string textValue;
    
    [SerializeField]
    private AudioSource audioSource;
    private bool isTriggered = false;
    private bool isGamePaused = false;
    private bool playerInRange = false;
    private bool canInteract = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("En rango");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {  
        dialogBox.SetActive(false);   
    }

    private void PauseGame()
    {
        dialogBox.SetActive(true);
        textBox.GetComponent<Text>().text = textValue;
        Time.timeScale = 0;
        isGamePaused = true;
    }
    
    private void ContinueGame()
    {
        dialogBox.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Track key releases to prevent multiple actions when pressing once
        if (Input.GetKeyUp(KeyCode.C))
        {
            canInteract = true;
        }
        
        if (canInteract)
        {
            // Show dialog
            if (Input.GetKeyDown(KeyCode.C) && playerInRange && !isTriggered && !isGamePaused)
            {
                canInteract = false;
                audioSource.Play();
                PauseGame();
                isTriggered = true;
                Debug.Log("Dialog shown");
            }
            
            // Hide dialog
            else if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C)) && isTriggered && isGamePaused)
            {
                canInteract = false;
                audioSource.Play();
                ContinueGame();
                isTriggered = false;
                Debug.Log("Dialog hidden");
            }
        }
    }
}