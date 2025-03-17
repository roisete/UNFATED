using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitHealth;
    public int unitMaxHealth;
    public int unitDefense;
    public int unitAttack;
    public int unitExp;

    public bool TakeDamage(int dmg)
    {
        unitHealth -= dmg - unitDefense;

        if (unitHealth <= 0)
        {
            return true;
        }
        return false;
    }
}
