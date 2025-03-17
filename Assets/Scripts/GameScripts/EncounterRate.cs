using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterRate : MonoBehaviour
{
    [SerializeField]
    private float encounterCheckInterval = 0.5f;
    [SerializeField]
    private int encounterProbability = 10; //10%

    private string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(CheckForEncounter());
    }

    IEnumerator CheckForEncounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(encounterCheckInterval);

            int randomValue = Random.Range(0, 100);

            //If random value is lesser than 10% at default, the battle starts
            if (randomValue < encounterProbability)
            {
                TriggerEncounter();
            }
        }
    }

    void TriggerEncounter()
    {
        if (sceneName == "StartScene")
        {
            int randomEncounter = Random.Range(0, 3);
            if (randomEncounter != 0)
            {
                CharacterStats.instance.ChangeScene("SlimeCombat"); //More prob to a weaker enemy
            }
            else
            {
                CharacterStats.instance.ChangeScene("Slime2Combat");
            }
        }
        else if (sceneName == "CaveScene")
        {
            int randomEncounter = Random.Range(0, 3);
            if (randomEncounter != 0)
            {
                CharacterStats.instance.ChangeScene("Slime3Combat"); //More prob to a weaker enemy
            }
            else
            {
                CharacterStats.instance.ChangeScene("Zombie2Combat");
            }
        }
    }
}