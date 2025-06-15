using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    public AudioSource sound;
    public float fadeOutFactor = 0.1f;
    public float fadeInFactor = 0.1f;
    public float maxVolume = 0.6f;
    private bool fadeInOut = false;

    void Start()
    {
        sound.volume = 0.0f;
    }

    void Update()
    {
        if (fadeInOut) //Fade In
        {      
            if (sound.volume < maxVolume)
            {
                sound.volume += fadeInFactor * Time.deltaTime;
                sound.volume = Mathf.Clamp(sound.volume, 0.0f, maxVolume);
            }
        }
        else //Fade Out
        {
            if (sound.volume > 0.0f)
            {
                sound.volume -= fadeOutFactor * Time.deltaTime;
                sound.volume = Mathf.Clamp(sound.volume, 0.0f, maxVolume);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeInOut = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeInOut = false;
        }
    }
}
