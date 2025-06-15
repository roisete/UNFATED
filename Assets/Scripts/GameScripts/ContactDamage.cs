using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject contactObject;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject damageSound;
    private AudioSource sound;
    private Animator anim;
    [SerializeField]
    private float contactAnimTime;
    [SerializeField]
    private bool hasAnim = false;
    [SerializeField]
    private Vector3 distance;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float knockbackTime = 0.2f;
    [SerializeField]
    private float knockbackSpeed = 5f;
    private bool isKnockingBack = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sound = damageSound.GetComponent<AudioSource>();
        if (hasAnim)
        {
            anim = contactObject.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isKnockingBack)
        {
            sound.Play();

            // Non mata
            if ((CharacterStats.instance.health > CharacterStats.instance.maxHealth / 20) && (CharacterStats.instance.health - damage > 0))
                CharacterStats.instance.health -= damage;
            StartCoroutine(ContactAnimation());
            StartCoroutine(Knockback());
        }
    }

    private IEnumerator Knockback()
    {
        isKnockingBack = true;

        Vector3 startPos = player.transform.position;
        Vector3 targetPos = startPos + distance;

        float elapsedTime = 0f;

        //Desactivar control
        if (PlayerController.instance != null)
            PlayerController.instance.enabled = false;

        //Interpolación lineal
        while (elapsedTime < knockbackTime)
        {
            elapsedTime += Time.deltaTime * knockbackSpeed;
            float progress = elapsedTime / knockbackTime;

            progress = 1f - (1f - progress) * (1f - progress);

            player.transform.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;
        }

        player.transform.position = targetPos;

        //Reactivar control
        if (PlayerController.instance != null)
            PlayerController.instance.enabled = true;

        isKnockingBack = false;
    }

    private IEnumerator ContactAnimation()
    {
        if (hasAnim)
        {
            anim.SetTrigger("Contact");
            yield return new WaitForSeconds(contactAnimTime);
            anim.SetTrigger("Exit");
        }
            
                
    }
}
