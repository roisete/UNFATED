using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages all the mechanics in a classic turn-based RPG battle

public enum BattleState {
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    FLED
}


public class BattleSystem : MonoBehaviour
{
    public GameObject player;
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

        dialogText.text = "Un " + enemyUnit.unitName + " apareció!";

        enemyName.text = enemyUnit.unitName;

        playerHUD.SetHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        Debug.Log("PLAYERTURN");
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.text = "Escoge:";
    }

    IEnumerator EnemyTurn()
    {
        int damageDealt = enemyUnit.unitAttack - CharacterStats.instance.defense;
        if (damageDealt <= 0)
        {
            damageDealt = 0;
        }
        //Attack animation
        enemyUnit.GetComponent<Animator>().SetTrigger("Attack");
        enemyUnit.GetComponent<AudioSource>().Play();
        CharacterStats.instance.health -= damageDealt;
        dialogText.text = "Recibiste " + enemyUnit.unitAttack + " de daño de " + enemyUnit.unitName + "!";
        yield return new WaitForSeconds(1f);
        playerHUD.hpText.text = CharacterStats.instance.health.ToString() + "/" + CharacterStats.instance.maxHealth.ToString(); 
        if (CharacterStats.instance.health <= 0)
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
        //Hurt animation
        enemyUnit.GetComponent<Animator>().SetTrigger("Hurt");
        enemyUnit.GetComponent<AudioSource>().Play();
        //Damage the enemy
        bool isEnemyDead = enemyUnit.TakeDamage(CharacterStats.instance.attack);
        dialogText.text = "Has hecho " + CharacterStats.instance.attack + " de daño a " + enemyUnit.unitName + "!";
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
        dialogText.text = "Te intentas defender!";
        CharacterStats.instance.defense += boost;
        yield return new WaitForSeconds(1f);
        if (boost == 0)
        {
            dialogText.text = "No te sirvió de nada!";
        }
        state = BattleState.ENEMYTURN;
        Debug.Log("ENEMYTURN");
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerFlee()
    {
        int flee = (int)Random.Range(0, 10);
        dialogText.text = "Intentas escapar!";
        yield return new WaitForSeconds(1f);
        if (flee >= 6)
        {
            state = BattleState.FLED;
            Debug.Log("FLED");
            StartCoroutine(EndBattle());
        }
        else
        {
            dialogText.text = "No lograste escapar!";
            yield return new WaitForSeconds(1f);
            state = BattleState.ENEMYTURN;
            Debug.Log("ENEMYTURN");
            StartCoroutine(EnemyTurn());
        }
        
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
            //Death animation
            enemyUnit.GetComponent<Animator>().SetTrigger("Death");
            enemyUnit.GetComponent<AudioSource>().Play();
            dialogText.text = "Ganaste!";
            CharacterStats.instance.addExp(enemyUnit.unitExp);
            yield return new WaitForSeconds(2f);
            if (CharacterStats.instance.checkLevelUp())
            {
                dialogText.text = "Has subido de nivel!";
                yield return new WaitForSeconds(2f);
            }
            OnBackToMenu();
        }
        else if (state == BattleState.LOST)
        {
            dialogText.text = "Has sido derrotado...";
            yield return new WaitForSeconds(3f);
            //Destroy the object with the tag Player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Destroy(player);
            }
            CharacterStats.instance.ChangeScene("Intro");
        }
        else if (state == BattleState.FLED)
        {
            dialogText.text = "Has escapado!";
            yield return new WaitForSeconds(3f);
            OnBackToMenu();
        }
    }

    public void OnBackToMenu()
    {
        if (sceneName == "SlimeCombat" || sceneName == "Plant1Combat" || sceneName == "Slime2Combat")
        {
            CharacterStats.instance.ChangeScene("StartScene");
        }
        else if (sceneName == "Slime3Combat" || sceneName == "Zombie2Combat")
        {
            CharacterStats.instance.ChangeScene("CaveScene");
        }
    }
}
