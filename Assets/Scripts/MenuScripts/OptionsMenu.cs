using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // Make a sounf bar with an Image component
    [SerializeField]
    private Image sound;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject gameBar;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true); 
        if (PlayerPrefs.HasKey("SoundLevel"))
        {
            float volume = PlayerPrefs.GetFloat("SoundLevel");
            sound.fillAmount = volume;
            AudioListener.volume = volume;
        }
        else
        {
            AudioListener.volume = sound.fillAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = gameBar.GetComponent<AudioSource>();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            audioSource.Play();
            sound.fillAmount -= 0.05f;
            PlayerPrefs.SetFloat("SoundLevel", sound.fillAmount);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            audioSource.Play();
            sound.fillAmount += 0.05f;
            PlayerPrefs.SetFloat("SoundLevel", sound.fillAmount);
        }
        AudioListener.volume = sound.fillAmount;

        if (Input.GetKeyDown(KeyCode.Escape)){
            menu.SetActive(false);
        }
    }
}
