using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    public float health;

    [SerializeField]
    public float defense;

    [SerializeField]
    public float attack;

    [SerializeField]
    public int level;

    public List<string> keyItems;
    public int itemIndex = 0;

    public static CharacterStats instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}
