using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public bool greenOrb = false;
    public bool redOrb = false;
    public bool blueOrb = false;
    public bool purpleOrb = false;
    public List<int> chestKeys = new();
    public List<string> keyItems;
    public List<string> healthItems;
    public static Items instance;

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

    //Método para saber se tes un item comprobando o seu tag
    public bool HasTagItem(GameObject kItem)
    {
        foreach (string item in keyItems)
        {
            if (kItem.CompareTag(item))
            {
                return true;
            }
        }
        return false;
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
