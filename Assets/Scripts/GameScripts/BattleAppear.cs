using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class BattleAppear : MonoBehaviour
{
    [SerializeField]
    private string textValue;
    [SerializeField]
    private GameObject dangerAudio;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !CharacterEvents.instance.plantDefeated)
        {
            StartCoroutine(Wait());
            gameObject.SetActive(false);
            CharacterStats.instance.ChangeScene("Plant1Combat");
            CharacterEvents.instance.plantDefeated = true;
        }
        if (CharacterEvents.instance.plantDefeated)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Wait()
    {
        AudioSource danger = dangerAudio.GetComponent<AudioSource>();
        danger.Play();
        yield return new WaitForSeconds(danger.clip.length);
    }
}
