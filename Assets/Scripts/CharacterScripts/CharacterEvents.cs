using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvents : MonoBehaviour
{
    public bool greenOrb = false;
    public bool redOrb = false;
    public bool blueOrb = false;
    public bool purpleOrb = false;
    public bool caveOpenedTriggered = false;
    public bool robotinWalkTriggered = false;
    public bool plantDefeated = false;

    public List<int> chestKeys = new();

    public static CharacterEvents instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Se existe outra instancia, destruíla para evitar duplicados
            Destroy(gameObject);
        }
    }

    //Métodos para controlar os cofres
    public void AddChestKey(int key)
    {
        if (!chestKeys.Contains(key))
        {
            chestKeys.Add(key);
        }
    }
    
    public bool HasChestKey(int key)
    {
        return chestKeys.Contains(key);
    }
}
