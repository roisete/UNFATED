using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject gameBar;
    private AudioSource audioSource;
    private const string SOUND_LEVEL_KEY = "SoundLevel";
    private const float VOLUME_STEP = 0.05f;
    [SerializeField]
    private Image sound;
    [SerializeField]

    [Header("Difficulty")]
    private Text story;
    [SerializeField]
    private Text easy;
    [SerializeField]
    private Text normal;
    [SerializeField]
    private Text hard;
    [SerializeField]
    

    void Start()
    {
        menu.SetActive(true);
        LoadSavedVolume();
        audioSource = gameBar.GetComponent<AudioSource>();
    }

    private void LoadSavedVolume()
    {
        if (PlayerPrefs.HasKey(SOUND_LEVEL_KEY))
        {
            //Carga o volume gardado en PlayerPrefs
            float volume = PlayerPrefs.GetFloat(SOUND_LEVEL_KEY);
            //Aplica o volume
            sound.fillAmount = volume;
            AudioListener.volume = volume;
        }
        else
        {
            //Carga o volume por defecto
            AudioListener.volume = sound.fillAmount;
        }
    }

    void Update()
    {
        //Subir/baixar volumen
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Baixa o volume
            audioSource.Play();
            sound.fillAmount -= VOLUME_STEP;
            PlayerPrefs.SetFloat(SOUND_LEVEL_KEY, sound.fillAmount);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Sube o volume
            audioSource.Play();
            sound.fillAmount += VOLUME_STEP;
            PlayerPrefs.SetFloat(SOUND_LEVEL_KEY, sound.fillAmount);
        }
        AudioListener.volume = sound.fillAmount;


        //Subir/baixar dificultade
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("HISTORIA");
            audioSource.Play();
            CharacterStats.instance.difficulty = 1;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("FÁCIL");
            audioSource.Play();
            CharacterStats.instance.difficulty = 2;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("NORMAL");
            audioSource.Play();
            CharacterStats.instance.difficulty = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            Debug.Log("DIFÍCIL");
            audioSource.Play();
            CharacterStats.instance.difficulty = 4;
        }

        //Que texto mostrar
        switch (CharacterStats.instance.difficulty)
        {
            case 1:
                story.enabled = true;
                easy.enabled = false;
                normal.enabled = false;
                hard.enabled = false;
                break;
            case 2:
                story.enabled = false;
                easy.enabled = true;
                normal.enabled = false;
                hard.enabled = false;
                break;
            case 3:
                story.enabled = false;
                easy.enabled = false;
                normal.enabled = true;
                hard.enabled = false;
                break;
            case 4:
                story.enabled = false;
                easy.enabled = false;
                normal.enabled = false;
                hard.enabled = true;
                break;
        }
        
        //Saír do menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
        }
    }
}
