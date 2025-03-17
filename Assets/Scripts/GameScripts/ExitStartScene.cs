using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStartScene : MonoBehaviour
{

    [SerializeField]
    private string scene;

    public GameObject player = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CharacterStats.instance.ChangeScene(scene);
        }
    }
}
