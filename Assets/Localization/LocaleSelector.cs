using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;
    [SerializeField]
    private AudioSource audioSource;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeLocale(0); //Inglés
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeLocale(1); //Galego
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeLocale(2); //Español
            audioSource.Play();
        }
    }

    //Cambia o idioma según o índice do idioma
    public void ChangeLocale(int localeIndex)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeIndex));
    }

    IEnumerator SetLocale(int _localeIndex)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeIndex];
        active = false;
    }
}
