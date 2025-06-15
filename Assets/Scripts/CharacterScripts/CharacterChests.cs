using System.Collections.Generic;
using UnityEngine;

public class ChracterChests : MonoBehaviour
{
    public List<int> chestKeys = new();

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
