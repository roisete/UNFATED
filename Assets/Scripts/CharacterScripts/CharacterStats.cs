using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //STATS DE PERSONAXE
    public string playerName;
    public int health;
    public int maxHealth;
    public int defense;
    public int initialDefense;
    public int attack;
    public int level;
    private float nextLevel = 10;
    public int exp;
    public int totalEXP;
    [Range (0,10000)]
    public int gold;

    //STATS DIFICULTADE
    public int difficulty = 2;
    public float encounterTimer = 0;


    public static CharacterStats instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addExp(int enemyExp)
    {
        exp += enemyExp;
        if (exp >= totalEXP)
        {
            LevelUp();
        }
    }

    public bool checkLevelUp()
    {
        if (exp >= totalEXP)
        {
            return true;
        }
        return false;
    }

    public void LevelUp()
    {
        int expRest = exp - totalEXP;
        level++;
        exp = expRest;
        totalEXP = (int)(nextLevel * (level * 1.2f));
        nextLevel *= 1.2f;
        maxHealth += (int)Random.Range(0, 6);
        health = maxHealth;
        defense += (int)Random.Range(0, 6);
        attack += (int)Random.Range(0, 6);
    }
}
