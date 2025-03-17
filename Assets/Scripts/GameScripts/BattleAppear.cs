using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class BattleAppear : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private string textValue;
    [SerializeField]
    private AudioSource audioSource;

    public GameObject player;

    private void Start()
    {
        dialogBox.SetActive(false);
    }


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

    IEnumerator Wait(){
        dialogBox.SetActive(true);
        audioSource.Play();
        textBox.GetComponent<Text>().text = textValue;
        yield return new WaitForSeconds(1f);
    }
}
