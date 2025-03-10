using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{

    [SerializeField]
    GameObject musicObject = new GameObject();

    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music = musicObject.GetComponent<AudioSource>();
        music.Play();
    }

    private void Awake()
    {
        DontDestroyOnLoad(musicObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
