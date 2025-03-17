using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWalkEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject robotin = new();
    [SerializeField]
    private GameObject gate = new();
    [SerializeField]
    private float speed = 2;


    private bool isTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && !CharacterEvents.instance.robotinWalkTriggered)
        {
            Vector3 pos = robotin.transform.position;
            pos.y += speed * Time.deltaTime;
            robotin.transform.position = pos;

        if(pos.y > -1)
        {
            gate.GetComponent<AudioSource>().Play();
            Destroy(robotin);
            CharacterEvents.instance.robotinWalkTriggered = true;
        }
        }
        if (CharacterEvents.instance.robotinWalkTriggered)
        {
            Destroy(robotin);
        }
        
    }
}
