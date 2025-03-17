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

    public GameObject player = new();

    private void Start()
    {
        dialogBox.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Wait());
            gameObject.SetActive(false);
            CharacterStats.instance.ChangeScene("Plant1Combat");
        }
    }

    IEnumerator Wait(){
        dialogBox.SetActive(true);
        audioSource.Play();
        textBox.GetComponent<Text>().text = textValue;
        yield return new WaitForSeconds(1f);
    }
}
