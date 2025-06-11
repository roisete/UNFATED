using UnityEngine;

public class CaveWalkEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject robotin;
    [SerializeField]
    private GameObject gateSound;
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
            gateSound.GetComponent<AudioSource>().Play();
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
