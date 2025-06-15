using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

//Estados nun turno
public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    FLED,
    END
}

public class BattleSystem : MonoBehaviour
{
    [Header("Characters")]
    public GameObject enemy;
    public Transform enemyPosition;
    Unit enemyUnit;
    private string enemyName;

    [Header("Localization Variables")]
    public LocalizeStringEvent dialogText;

    [Header("Battle Variables")]
    private string sceneName;
    public BattleHUD playerHUD;
    [SerializeField]
    private BattleState state;
    private bool isWon = false;

    [Header("Battle Animators")]
    [SerializeField]
    private Animator pdmg;
    [SerializeField]
    private Animator mdmg;
    [SerializeField]
    private Animator def;
    [SerializeField]
    private Animator healthPot;
    [SerializeField]
    private Animator flee;
    [SerializeField]
    private Animator exit;

    [Header("Battle Sounds")]
    [SerializeField]
    private AudioSource pdmgSound;
    [SerializeField]
    private AudioSource mdmgSound;
    [SerializeField]
    private AudioSource defSound;
    [SerializeField]
    private AudioSource healSound;
    [SerializeField]
    private AudioSource fleeSound;
    [SerializeField]
    private AudioSource commandSound;
    [SerializeField]
    private AudioSource victory;
    [SerializeField]
    private AudioSource defeat;
    [SerializeField]
    private AudioSource theme;

    [Header("Flee Bar")]
    [SerializeField]
    private Image fill;
    [SerializeField]
    private float fillSpeed = 0.5f;
    [SerializeField]
    private float emptySpeed = 0.8f;
    private float currentFillAmount = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject enemyGO = Instantiate(enemy, enemyPosition);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyName = enemyUnit.unitName;
        fill.fillAmount = currentFillAmount;
        //Dificultad
        switch (CharacterStats.instance.difficulty)
        {
            case 2:
                enemyUnit.unitMaxHealth = (int)(enemyUnit.unitMaxHealth * 0.7f);
                enemyUnit.unitHealth = (int)(enemyUnit.unitHealth * 0.7f);
                enemyUnit.unitDefense = (int)(enemyUnit.unitDefense * 0.5f);
                enemyUnit.unitMDefense = (int)(enemyUnit.unitMDefense * 0.5f);
                enemyUnit.unitAttack = (int)(enemyUnit.unitAttack * 0.7f);
                enemyUnit.unitExp = (int)(enemyUnit.unitExp * 0.5f);
                break;
            case 3:
                break;
            case 4:
                enemyUnit.unitMaxHealth = (int)(enemyUnit.unitMaxHealth * 1.8f);
                enemyUnit.unitHealth = (int)(enemyUnit.unitHealth * 1.8f);
                enemyUnit.unitDefense = (int)(enemyUnit.unitDefense * 2f);
                enemyUnit.unitMDefense = (int)(enemyUnit.unitMDefense * 1.4f);
                enemyUnit.unitAttack = (int)(enemyUnit.unitAttack * 1.6f);
                enemyUnit.unitExp = (int)(enemyUnit.unitExp * 1.5f);
                break;
        }

        dialogText.StringReference.SetReference("Combat Collection", "EnemyApproach");
        playerHUD.SetHUD();

        yield return new WaitForSeconds(3f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.StringReference.SetReference("Combat Collection", "TurnPrompt");
        dialogText.RefreshString();
    }

    void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                commandSound.Play();
                StartCoroutine(PlayerAttack());
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                commandSound.Play();
                StartCoroutine(PlayerMagic());
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                commandSound.Play();
                StartCoroutine(PlayerDefend());
            }

            else if (Input.GetKeyDown(KeyCode.V))
            {
                commandSound.Play();
                StartCoroutine(PlayerUseItem());
            }
            //Manter pulsado para encher a barra de escape
            if (Input.GetKey(KeyCode.F))
            {
                currentFillAmount += fillSpeed * Time.deltaTime;
                if (currentFillAmount >= 1f)
                {
                    currentFillAmount = 0f;
                    commandSound.Play();
                    StartCoroutine(PlayerFlee());
                }
            }
            else if (currentFillAmount > 0f)
            {
                currentFillAmount -= emptySpeed * Time.deltaTime;
                if (currentFillAmount < 0f)
                    currentFillAmount = 0f;
            }
            fill.fillAmount = currentFillAmount;
        }
        else if (state == BattleState.END)
        {

            if (CharacterStats.instance.health == 0 && Input.GetKeyDown(KeyCode.C))
            {
                commandSound.Play();
                exit.SetTrigger("Start");
                Invoke("ToIntro", 2f);
            }
            //Ganache
            else if (isWon && Input.GetKeyDown(KeyCode.C))
            {
                commandSound.Play();
                OnBackToWorld();
            }
        }
        else return;
    }

    IEnumerator PlayerAttack()
    {
        int physDamage = Random.Range(CharacterStats.instance.attack - 1, CharacterStats.instance.attack + 3) - (enemyUnit.unitDefense/2);
        //Engadir crítico
        if (physDamage < 1) physDamage = 1;
        StartCoroutine(Anim(pdmg, pdmgSound, 0.5f, 0.583f));
        Invoke("AnimHurt", 0.75f);
        bool isDead = enemyUnit.TakeDamage(physDamage);
        dialogText.StringReference.SetReference("Combat Collection", "PlayerDealtDamage");
        dialogText.RefreshString();

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDefend()
    {
        int chance = Random.Range(0, 10);
        //Boost de defensa entre un 10% e un 30%
        int boost = Random.Range((int)(CharacterStats.instance.defense * 0.1f), (int)(CharacterStats.instance.defense * 0.3f));

        dialogText.StringReference.SetReference("Combat Collection", "TryDef");
        dialogText.RefreshString();

        yield return new WaitForSeconds(1.5f);

        if (chance >= 5)
        {
            StartCoroutine(Anim(def, defSound, 0.5f, 0.750f));
            CharacterStats.instance.defense += boost;
            dialogText.StringReference.SetReference("Combat Collection", "DefSuccess");
            dialogText.RefreshString();
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogText.StringReference.SetReference("Combat Collection", "DefFail");
            dialogText.RefreshString();
            yield return new WaitForSeconds(1f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator PlayerMagic()
    {
        if (CharacterStats.instance.mana < 5)
        {
            dialogText.StringReference.SetReference("Combat Collection", "NoMana");
            dialogText.RefreshString();
            yield return new WaitForSeconds(3f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            CharacterStats.instance.mana -= 5;
            int magicDamage = Random.Range(CharacterStats.instance.mAttack - 1, CharacterStats.instance.mAttack + 3) - enemyUnit.unitMDefense;
            //Engadir crítico
            StartCoroutine(Anim(mdmg, mdmgSound, 0.5f, 1.083f));
            Invoke("AnimHurt", 1f);
            bool isDead = enemyUnit.TakeDamage(magicDamage);
            dialogText.StringReference.SetReference("Combat Collection", "PlayerUsedMagic");
            dialogText.RefreshString();
            playerHUD.SetHUD();
            yield return new WaitForSeconds(3f);

            if (isDead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator PlayerUseItem()
    {
        if (Items.instance.HasNormalItem("healthPotion"))
        {
            if (CharacterStats.instance.health >= 2 * CharacterStats.instance.maxHealth / 3)
            {
                dialogText.StringReference.SetReference("Combat Collection", "HealthFull");
                dialogText.RefreshString();
            }
            else
            {
                dialogText.StringReference.SetReference("Combat Collection", "UsedPotion");
                dialogText.RefreshString();
            }

            StartCoroutine(Anim(healthPot, healSound, 0.5f, 1.333f));
            CharacterStats.instance.health += CharacterStats.instance.maxHealth / 3;
            if (CharacterStats.instance.health > CharacterStats.instance.maxHealth)
                CharacterStats.instance.health = CharacterStats.instance.maxHealth;
            //Elimínase o item da "mochila"
            Items.instance.healthItems.Remove("healthPotion");
            playerHUD.SetHUD();
            yield return new WaitForSeconds(1.5f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogText.StringReference.SetReference("Combat Collection", "NoPot");
            dialogText.RefreshString();
            playerHUD.SetHUD();
            yield return new WaitForSeconds(1f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerFlee()
    {
        //Non podes huir de bosses e minibosses, irase expandindo (lista de strings coas escenas)
        if (sceneName == "Plant1Combat")
        {
            dialogText.StringReference.SetReference("Combat Collection", "FleeFail");
            dialogText.RefreshString();
            yield return new WaitForSeconds(1.5f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            int chance = Random.Range(0, 10);
            dialogText.StringReference.SetReference("Combat Collection", "TryFlee");
            dialogText.RefreshString();

            yield return new WaitForSeconds(1f);

            if (chance >= 7)
            {
                state = BattleState.FLED;
                StartCoroutine(EndBattle());
            }
            else
            {
                dialogText.StringReference.SetReference("Combat Collection", "FleeFail");
                dialogText.RefreshString();
                yield return new WaitForSeconds(1.5f);
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        Invoke("AnimEnenyAttack", 0.5f);
        int damageDealt = Random.Range(enemyUnit.unitAttack - 1, enemyUnit.unitAttack + 3) - CharacterStats.instance.defense;
        if (damageDealt > CharacterStats.instance.defense)
        {
            CharacterStats.instance.health -= damageDealt;
            if (CharacterStats.instance.health < 0)
                CharacterStats.instance.health = 0;
        }
        else
        {
            CharacterStats.instance.health -= 1;
        }
        dialogText.StringReference.SetReference("Combat Collection", "DamageReceived");
        dialogText.RefreshString();
        playerHUD.SetHUD();
        yield return new WaitForSeconds(2f);

        if (CharacterStats.instance.health <= 0)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            BossesDefeated();
            isWon = true;
            theme.Stop();
            victory.Play();
            //Música victoria
            Invoke("AnimDeath", 0.5f);

            dialogText.StringReference.SetReference("Combat Collection", "Victory");
            dialogText.RefreshString();
            yield return new WaitForSeconds(1f);

            CharacterStats.instance.addExp(enemyUnit.unitExp);

            if (CharacterStats.instance.checkLevelUp())
            {
                StartCoroutine(ShowLvlUp());
            }
            state = BattleState.END;
        }
        else if (state == BattleState.LOST)
        {
            theme.Stop();
            defeat.Play();
            dialogText.StringReference.SetReference("Combat Collection", "Defeat");
            dialogText.RefreshString();
            yield return new WaitForSeconds(3f);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Destroy(player);
            }
            state = BattleState.END;

        }
        else if (state == BattleState.FLED)
        {
            StartCoroutine(Anim(flee, fleeSound, 0.5f, 1.5f));
            dialogText.StringReference.SetReference("Combat Collection", "FleeSuccess");
            dialogText.RefreshString();
            yield return new WaitForSeconds(1.5f);
            OnBackToWorld();
        }
        else
        {
            Debug.LogError("Volvemos ao turno do xogador");
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private IEnumerator ShowLvlUp()
    {
        dialogText.StringReference.SetReference("Combat Collection", "LevelUp");
        dialogText.RefreshString();
        yield return new WaitForSeconds(2f);
    }

    private void OnBackToWorld()
    {
        exit.SetTrigger("Start");
        Invoke("LoadScene", 1.5f);
    }

    void ToIntro()
    {
        SceneSpawnManager.instance.ChangeScene("IntroStart");
    }

    private void LoadScene()
    {
        if (sceneName == "SlimeCombat" || sceneName == "Plant1Combat" || sceneName == "Slime2Combat")
        {
            SceneSpawnManager.instance.ChangeScene("StartScene");
        }
        else if (sceneName == "Slime3Combat" || sceneName == "Zombie2Combat")
        {
            SceneSpawnManager.instance.ChangeScene("CaveScene");
        }
    }

    /*MÉTODOS DE ANIMACIÓNS*/
    void AnimHurt()
    {
        enemyUnit.GetComponent<Animator>().SetTrigger("Hurt");
    }

    void AnimDeath()
    {
        enemyUnit.GetComponent<Animator>().SetTrigger("Death");
    }

    void AnimEnenyAttack()
    {
        enemyUnit.GetComponent<Animator>().SetTrigger("Attack");
        enemyUnit.GetComponent<AudioSource>().Play();
    }

    IEnumerator Anim(Animator animator, AudioSource audio, float time, float animTime)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Start");
        audio.Play();
        yield return new WaitForSeconds(animTime);
        if (animator.name != "MCHUD")
            animator.SetTrigger("End");
    }

    void BossesDefeated()
    {
        if(sceneName == "Plant1Combat")
            CharacterEvents.instance.plantDefeated = true;   
    }
}