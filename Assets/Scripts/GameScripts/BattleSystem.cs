using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState {
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}



public class BattleSystem : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPosition;

    Unit enemyUnit;

    public Text enemyName;
    public Text dialogText;

    private string sceneName;

    public BattleHUD playerHUD;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        // Set up enemies, players, and any other necessary game objects
        GameObject enemyGO = Instantiate(enemy, enemyPosition);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = "A wild " + enemyUnit.unitName + " appears!";

        enemyName.text = enemyUnit.unitName;

        playerHUD.SetHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        Debug.Log("PLAYERTURN");
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action:";
    }

    IEnumerator EnemyTurn()
    {
        bool isPlayerDead = CharacterStats.instance.TakeDamage(enemyUnit.unitAttack);
        dialogText.text = "You took " + enemyUnit.unitAttack + " damage from " + enemyUnit.unitName + "!";
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(CharacterStats.instance.health);
        if (isPlayerDead)
        {
            state = BattleState.LOST;
            Debug.Log("LOST");
            StartCoroutine(EndBattle());
        }else
        {
            state = BattleState.PLAYERTURN;
            Debug.Log("PLAYERTURN");
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy
        bool isEnemyDead = enemyUnit.TakeDamage(CharacterStats.instance.attack);
        dialogText.text = "You attack dealt " + CharacterStats.instance.attack + " damage to " + enemyUnit.unitName + "!";
        CharacterStats.instance.defense = CharacterStats.instance.initialDefense;
        yield return new WaitForSeconds(1f);

        //Check if the enemy is dead
        //Change state based on what happened
        if (isEnemyDead)
        {
            state = BattleState.WON;
            Debug.Log("WON");
            StartCoroutine(EndBattle());
        }else
        {
            state = BattleState.ENEMYTURN;
            Debug.Log("ENEMYTURN");
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDefend()
    {
        int boost = (int)Random.Range(0, 5);
        dialogText.text = "You defended yourself!";
        CharacterStats.instance.defense += boost;
        yield return new WaitForSeconds(1f);
        if (boost == 0)
        {
            dialogText.text = "Your defense failed!";
        }
        state = BattleState.ENEMYTURN;
        Debug.Log("ENEMYTURN");
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerFlee()
    {
        int flee = (int)Random.Range(0, 10);
        dialogText.text = "You try to flee!";
        yield return new WaitForSeconds(1f);
        if (flee >= 6)
        {
            dialogText.text = "Your fled successfully!";
        }
        state = BattleState.WON;
        Debug.Log("WON");
    }
    

    public void OnAttack()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    public void onDefend()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerDefend());
    }

    public void onFlee()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerFlee());
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogText.text = "You have won!";
            CharacterStats.instance.addExp(enemyUnit.unitExp);
            yield return new WaitForSeconds(3f);
            OnBackToMenu();
        }
        else if (state == BattleState.LOST)
        {
            dialogText.text = "You have lost!";
            yield return new WaitForSeconds(3f);
            OnBackToMenu();
            
        }
    }

    public void OnBackToMenu()
    {
        if (sceneName == "SlimeCombat" || sceneName == "PlantCombat")
        {
            CharacterStats.instance.ChangeScene("StartScene");
        }
        else if (sceneName == "ElecSlimeCombat")
        {
            CharacterStats.instance.ChangeScene("CaveScene");
        }
    }
}
