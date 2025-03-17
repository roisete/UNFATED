using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class EncounterRate : MonoBehaviour
{
    public int index = 0;
    private string sceneName;

    public int rate;
    public int rate2;
    
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(GetEncounter());
    }


    // Every second we increase the rate. This is a ver raw temporary method, I'll change it in the future
    IEnumerator GetEncounter()
    {
        rate =  Random.Range(index, 60);
        rate2 = Random.Range(index, 60);
        if(rate2 == rate)
        {
            Debug.Log("Encounter!");
            yield return new WaitForSeconds(0.5f);
            // Spawn encounter here
            if (sceneName == "StartScene" && rate <= 1500)
            {
                CharacterStats.instance.ChangeScene("SlimeCombat");
            }
            else if (sceneName == "StartScene" && rate > 1500)
            {
                CharacterStats.instance.ChangeScene("Slime2Combat");
            }
            else if (sceneName == "CaveScene")
            {
                    CharacterStats.instance.ChangeScene("Slime3Combat");
            }
            rate = 0;
            rate2 = 0;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            rate++;
            rate2++;
        }
    }
}
