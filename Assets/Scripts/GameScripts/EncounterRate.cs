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
    [SerializeField]
    private float timerMinOG;
    [SerializeField]
    private float timerMaxOG;
    [SerializeField]
    private Image fill;
    public float timer;
    private string sceneName;


    void Start()
    {
        //Para saber que enemigos spawnear
        sceneName = SceneManager.GetActiveScene().name;
        fill.fillAmount = 0;
    }

    //Se entras na zona de spawn de enemigos, terás un tempo no que aparezan (mostrar con HUD)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CharacterStats.instance.difficulty != 0)
            {
                StopAllCoroutines();
                StartCoroutine(CheckForEncounter());
            }
        }
    }

    //Se saes da zona, para a corutina pero mantén o avanzado ata que spawnee
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(StopEncounter());
        }

    }

    IEnumerator CheckForEncounter()
    {
        switch (CharacterStats.instance.difficulty)
        {
            case 1:
                timer = Random.Range(timerMin * 1.5f, timerMax * 1.5f);
                break;
            case 2:
                timer = Random.Range(timerMin, timerMax);
                break;
            case 3:
                timer = Random.Range(timerMin * 0.6f, timerMax * 0.6f);
                break;
        }
        Debug.Log(timer + "segundos de timer");
        while (CharacterStats.instance.encounterTimer < timer)
        {
            //Seg antes do combate
            yield return new WaitForSeconds(1);
            CharacterStats.instance.encounterTimer++;
            fill.fillAmount = CharacterStats.instance.encounterTimer / timer;
        }
        CharacterStats.instance.encounterTimer = 0;
        fill.fillAmount = 0; //Reset
        TriggerEncounter();
    }

    IEnumerator StopEncounter()
    {
        while (CharacterStats.instance.encounterTimer > 0)
        {
            yield return new WaitForSeconds(1);
            CharacterStats.instance.encounterTimer--;
            fill.fillAmount = CharacterStats.instance.encounterTimer / timer;
        }
    }

    void TriggerEncounter()
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
}