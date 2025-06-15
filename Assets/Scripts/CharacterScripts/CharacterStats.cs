using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public string playerName;
    [Range (1,999)]
    public int health;
    [Range (1,999)]
    public int maxHealth;
    [Range (1,999)]
    public int mana;
    [Range (1,999)]
    public int maxMana;
    [Range (1,999)]
    public int defense;
    [Range (1,999)]
    public int initialDefense;
    [Range (1,999)]
    public int attack;
    [Range (1,999)]
    public int mAttack;
    [Range (1,99)]
    public int level;
    public int exp;
    public int totalEXP;
    [Range (0,10000)]
    public int gold;

    [Header("Difficulty")]
    [Range (1,4)]
    public int difficulty;

    [Header("HUD")]
    public Image healthBar;
    public Image manaBar;


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
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (healthBar == null)
        {
            healthBar = GameObject.FindGameObjectWithTag("Health").GetComponent<Image>();
        }
        if (manaBar == null)
        {
            manaBar = GameObject.FindGameObjectWithTag("Mana").GetComponent<Image>();
        }
        healthBar.fillAmount = (float)health / (float)maxHealth;
        manaBar.fillAmount = (float)mana / (float)maxMana;
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
        int healthBoost= Random.Range(2, 6);
        int manaBoost = Random.Range(1, 4);
        int expRest = exp - totalEXP;
        level++;
        exp = expRest;
        totalEXP = 100 + (int)(Mathf.Pow(level, 1.8f) * 10); //Progresión clásica
        maxHealth += healthBoost;
        maxMana += manaBoost;
        defense += Random.Range(1, 3);
        attack += Random.Range(1, 3);
        mAttack += Random.Range(1, 3);
    }
}
