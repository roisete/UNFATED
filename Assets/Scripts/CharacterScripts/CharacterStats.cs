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

    // For saving the position on every scene change (example: returning to overworld after a battle)
    [System.Serializable]
    public class SpawnPosition
    {
        public string sceneName;
        public Vector2 position;
    }

    [SerializeField]
    private List<SpawnPosition> scenePositionsList = new List<SpawnPosition>();
    public List<string> keyItems;
    public int itemIndex = 0;

    public static CharacterStats instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }

    //Stats Functions


    //Scene Functions
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifies if there is a saved position for the scene
        SpawnPosition savedPosition = scenePositionsList.Find(sp => sp.sceneName == scene.name);
        if (savedPosition != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 spawnPosition = savedPosition.position + new Vector2(0, -0.5f); // Prevents looping
                player.transform.position = spawnPosition;
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Saves last position before changing scene
    public void SavePositionForCurrentScene(Vector2 position)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SpawnPosition existingPosition = scenePositionsList.Find(sp => sp.sceneName == currentSceneName);

        if (existingPosition != null)
        {
            // Update existing position
            existingPosition.position = position;
        }
        else
        {
            // Add new position
            scenePositionsList.Add(new SpawnPosition { sceneName = currentSceneName, position = position });
        }
    }

    // Public method for changing scenes
    public void ChangeScene(string sceneName)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SavePositionForCurrentScene(player.transform.position);
        }
        SceneManager.LoadScene(sceneName);
    }
}
