using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public string playerName;
    public int health;
    public int maxHealth;
    public int defense;
    public int initialDefense;
    public int attack;
    public int level;
    public int exp;
    public int totalEXP;
    private float nextLevel = 10;

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
        defense += (int)Random.Range(0, 6);;
        attack += (int)Random.Range(0, 6);;
    }

    //Para gardar a posición do xogador ao cambiar de escena
    [System.Serializable]
    public class SpawnPosition
    {
        public string sceneName;
        public Vector2 position;
    }

    [SerializeField]
    private List<SpawnPosition> scenePositionsList = new List<SpawnPosition>();
    public List<string> keyItems;
    public List<string> healthItems;

    public static CharacterStats instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            //Destrúe un duplicado
            Destroy(gameObject);
        }
    }

    //FUNCIÓNS DE STATS


    //FUNCIÓNS DE ESCENAS
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Verifica se hai unha posición guardada para a escena cargada
        SpawnPosition savedPosition = scenePositionsList.Find(sp => sp.sceneName == scene.name);
        if (savedPosition != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 spawnPosition = savedPosition.position + new Vector2(0, -0.5f); // Prevén que o xogador non spawnee no trigger
                player.transform.position = spawnPosition;
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Garda a posición do xogador na escena actual
    public void SavePositionForCurrentScene(Vector2 position)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SpawnPosition existingPosition = scenePositionsList.Find(sp => sp.sceneName == currentSceneName);

        if (existingPosition != null)
        {
            //Actualiza a posición existente
            existingPosition.position = position;
        }
        else
        {
            //Engade unha nova posición á lista de spawns
            scenePositionsList.Add(new SpawnPosition { sceneName = currentSceneName, position = position });
        }
    }

    //Cambio de escena e garda a posición do xogador
    public void ChangeScene(string sceneName)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SavePositionForCurrentScene(player.transform.position);
        }
        SceneManager.LoadSceneAsync(sceneName);
    }
}
