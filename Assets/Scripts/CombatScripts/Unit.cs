using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    public string unitName;
    [Range (1,999)]
    public int unitHealth;
    [Range (1,999)]
    public int unitMaxHealth;
    [Range (1,999)]
    public int unitDefense;
    [Range (1,999)]
    public int unitMDefense;
    [Range (1,999)]
    public int unitAttack;
    [Range (1,9999)]
    public int unitExp;

    public bool TakeDamage(int dmg)
    {
        unitHealth -= dmg;

        if (unitHealth <= 0)
        {
            return true;
        }
        return false;
    }
}
