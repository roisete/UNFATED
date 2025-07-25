using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitleMenu : MonoBehaviour
{
    //COIDADO CON PAUSAR, TER A FLECHA ANIMADA A HORA DE ESCOLLER A OPCION DE XOGAR. SOLUCIONARASE PRONTO
    [Header("Menu")]
    [SerializeField]
    private List<GameObject> menuOptions;
    [SerializeField]
    private GameObject menuArrow;
    [SerializeField]
    private GameObject optionsMenu;
    private int index = 0;
    private Vector2 pos = new();

    [Header("Music")]
    [SerializeField]
    GameObject menuTheme;
    [SerializeField]
    GameObject playMusic;

    AudioSource menu;
    AudioSource play;
    private AudioSource audioSource;

    //MENU PARA SELECCIONAR OPCIONES
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pos.x = menuArrow.GetComponent<SpriteRenderer>().transform.position.x;
        pos.y = menuArrow.GetComponent<SpriteRenderer>().transform.position.y;
        optionsMenu.SetActive(false);
        menu = menuTheme.GetComponent<AudioSource>();
        play = playMusic.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            switch (index)
            {
                case 0:
                    StartCoroutine(LetsPlay());
                    break;
                case 1:
                    audioSource.Play();
                    optionsMenu.SetActive(true);
                    break;
                case 2:
                    audioSource.Play();
                    Application.Quit();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 0.6f;
            menuArrow.GetComponent<SpriteRenderer>().transform.position = pos;
            index++;
            if (index >= menuOptions.Count)
            {
                index = 0;
                pos.y = 0;
                menuArrow.GetComponent<SpriteRenderer>().transform.position = pos;
            }
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 0.6f;
            menuArrow.GetComponent<SpriteRenderer>().transform.position = pos;
            index--;
            if (index < 0)
            {
                index = menuOptions.Count - 1;
                pos.y = 1;
                menuArrow.GetComponent<SpriteRenderer>().transform.position = pos;
            }
            audioSource.Play();
        }
    }

    IEnumerator LetsPlay(){
        menu.Stop();
        play.Play();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("RobotinJrDialog");
    }
}
