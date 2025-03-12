using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{

    [SerializeField]
    GameObject menuTheme = new GameObject();
    [SerializeField]
    GameObject playMusic = new GameObject();

    AudioSource menu;
    AudioSource play;



    // Start is called before the first frame update
    void Start()
    {
        menu = menuTheme.GetComponent<AudioSource>();
        play = playMusic.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
