using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string scene;
    // [SerializeField]
    // private Animator transitionAnimator;
    // [SerializeField]
    // private GameObject steps;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStats.instance.ChangeScene(scene);
        }
    }

    // private IEnumerator LoadScene()
    // {
    //     yield return new WaitForSeconds(2f);
    //     CharacterStats.instance.ChangeScene(scene);
    // }
}
