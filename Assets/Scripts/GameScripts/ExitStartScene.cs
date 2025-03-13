using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStartScene : MonoBehaviour
{

    [SerializeField]
    string scene;

    [SerializeField]
    public GameObject player = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(scene);
        }
    }
}
