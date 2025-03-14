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

}
