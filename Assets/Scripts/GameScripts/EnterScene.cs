using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScene : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField]
    private Animator anim;
    [Header("Loading Elements")]
    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private List<GameObject> loadSprites = new();

    void Start()
    {
        loading.SetActive(false);
        foreach (GameObject sprite in loadSprites)
        {
            sprite.SetActive(false);
        }
        StartCoroutine(Enter());
    }

    private IEnumerator Enter()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        anim.gameObject.SetActive(false);
    }
}
