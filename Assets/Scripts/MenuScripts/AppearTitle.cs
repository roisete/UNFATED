using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppearMenu : MonoBehaviour
{
    //Script for showing the menu after a few seconds

    [SerializeField]
    GameObject blackScreen;

    [SerializeField]
    float menuAppear = 4.5f;
    [SerializeField]
    float blackIntro = 7f;


    

    // Start is called before the first frame update
    private void Start()
    {
        blackScreen.gameObject.SetActive(true);
        StartCoroutine(DisableBlackScreen());
    }

    IEnumerator DisableBlackScreen()
    {
        yield return new WaitForSeconds(blackIntro);
        blackScreen.gameObject.SetActive(false);
        StartCoroutine(EnableUI());
    }

    IEnumerator EnableUI()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(menuAppear);
        SceneManager.LoadScene("MainMenu");
    }




}
