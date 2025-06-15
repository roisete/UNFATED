using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header ("Scene Change Settings")]
    [SerializeField]
    private string scene;
    [SerializeField]
    private Vector2 spawn;
    [Header ("Animation Settings")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    //Coherencia a donde mira o xogador
    private string headingDirecion;
    [SerializeField]
    private string lastDirection;
    [SerializeField]
    private GameObject steps;
    private AudioSource sound;
    [Header("Loading Elements")]
    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private List<GameObject> loadSprites = new();

    void Start()
    {
        foreach (GameObject load in loadSprites)
        {
            load.SetActive(false);
        }
        sound = steps.GetComponent<AudioSource>();
        loading.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sound.Play();
            anim.SetTrigger("Start");
            //Desactivar control e animaciones
            if (PlayerController.instance != null)
                PlayerController.instance.animator.SetBool(lastDirection, false);
                PlayerController.instance.animator.SetBool(headingDirecion, true);
                PlayerController.instance.enabled = false;
            Invoke("LoadScene", 1f);
        }
    }

    private void LoadScene()
    {  
        //Reactivar control
        if (PlayerController.instance != null)
            PlayerController.instance.enabled = true;
        int index = Random.Range(0, loadSprites.Count);
        loadSprites[index].SetActive(true);
        loading.SetActive(true);
        if (SceneSpawnManager.instance != null)
        {
            SceneSpawnManager.instance.ChangeScene(scene, spawn);
        }
    }
}
