using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuCombat : MonoBehaviour
{
    [SerializeField]
    private Image sound;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject gameBar;
    private AudioSource audioSource;
    private const string SOUND_LEVEL_KEY = "SoundLevel";
    private const float VOLUME_STEP = 0.05f;

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

        //Saír do menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
        }
    }
}
