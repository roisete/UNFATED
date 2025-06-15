using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
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

    //Método para saber se tes un item comprobando o seu tag. Cambiar por parámetro string
    public bool HasTagItem(GameObject kItem)
    {
        foreach (string item in keyItems)
        {
            if (kItem.CompareTag(item))
                return true;
        }
        return false;
    }

    //Comprobar se tes un item común (pocións, etc.)
    public bool HasNormalItem(string nItem)
    {
        foreach (string item in healthItems)
        {
            if (nItem == item)
                return true;
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
