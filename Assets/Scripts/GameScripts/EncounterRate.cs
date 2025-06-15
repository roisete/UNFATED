using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//BUGUÉASE DE MOMENTO CON 2 COLLIDERS NA MESMA ESCENA CON ESTE SCRIPT, DE MOMENTO UN POR ZONA

public class EncounterRate : MonoBehaviour
{
    [SerializeField]
    private float timerMin;
    [SerializeField]
    private float timerMax;
    private float timer;
    private bool isEncounter;
    private string sceneName;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject exclamation;

    [Header("Fill")]
    [SerializeField]
    private Image fill;
    private float fillSpeed;
    private float emptySpeed;
    private float currentFillAmount = 0f;


    void Start()
    {
        //Para saber que enemigos spawnear
        sceneName = SceneManager.GetActiveScene().name;
        fill.fillAmount = 0;
        if (exclamation.gameObject == null)
        {
            exclamation = GameObject.FindWithTag("Exclamation");
        }
        exclamation.SetActive(false);
    }

    //Se entras na zona de spawn de enemigos, terás un tempo no que aparezan (mostrar con HUD)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Buscar algunha forma de que ao quedar quieto se conxele o radar
            timer = Random.Range(timerMin, timerMax);
            Debug.Log(timer + " segundos de timer");
            fillSpeed = 1 / timer;
            emptySpeed = fillSpeed * 0.75f;
            if (CharacterStats.instance.difficulty != 1)
            {
                isEncounter = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEncounter = false;
        }
    }

    void Update()
    {
        if (isEncounter && CharacterStats.instance.difficulty != 1)
        {
            currentFillAmount += fillSpeed * Time.deltaTime;
            if (currentFillAmount >= 1f)
            {
                currentFillAmount = 0f;
                TriggerEncounter();
            }
        }
        else
        {
            currentFillAmount -= emptySpeed * Time.deltaTime;
            if (currentFillAmount < 0f)
                currentFillAmount = 0f;
        }
        fill.fillAmount = currentFillAmount;
    }

    void TriggerEncounter()
    {
        StartCoroutine(Exclamation());
        sound.Play();
        anim.SetTrigger("Start");
        Invoke("LoadScene", 1.6f);
    }

    void LoadScene()
    {
        if (sceneName == "StartScene")
        {
            int randomEncounter = Random.Range(0, 3);
            if (randomEncounter != 0)
            {
                SceneSpawnManager.instance.ChangeScene("SlimeCombat"); //Máis prob de enemigos débiles
            }
            else
            {
                SceneSpawnManager.instance.ChangeScene("Slime2Combat");
            }
        }
        else if (sceneName == "CaveScene")
        {
            int randomEncounter = Random.Range(0, 3);
            if (randomEncounter != 0)
            {
                SceneSpawnManager.instance.ChangeScene("Slime3Combat"); //Máis prob de enemigos débiles
            }
            else
            {
                SceneSpawnManager.instance.ChangeScene("Zombie2Combat");
            }
        }
    }

    private IEnumerator Exclamation()
    {
        exclamation.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        exclamation.SetActive(false);
    }
}