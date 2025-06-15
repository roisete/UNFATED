using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesRead : MonoBehaviour
{
    //Mesma lóxica que KeyItems pero separados para facilitar lectura
    public List<string> notes = new();
    public static NotesRead instance;

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

    public bool isRead(string note)
    {
        if (notes.Contains(note))
        {
            return true;
        }
        return false;
    }
}
