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
    private float nextLevel = 15;

    public void addExp(int enemyExp)
    {
        exp += enemyExp;
        if (exp >= totalEXP)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        exp = 0;
        totalEXP = (int)(nextLevel * (level * 1.2f));
        nextLevel *= 1.2f;
        maxHealth += (int)Random.Range(0, 6);
        health = maxHealth;
        defense += (int)Random.Range(0, 6);;
        attack += (int)Random.Range(0, 6);;
    }

    // For saving the position on every scene change (example: returning to overwold after a battle)
    
    public Dictionary<string, Vector2> scenePositions;
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
    public bool TakeDamage(int dmg)
    {
        health -= dmg - defense;

        if (health <= 0)
        {
            return true;
        }
        return false;
    }


    //Scene Functions
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Verifies if there is a scene position
        if (scenePositions.ContainsKey(scene.name))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 savedPosition = scenePositions[scene.name];
                Vector2 spawnPosition = savedPosition + new Vector2(0, -0.5f); // Prevents looping
                player.transform.position = spawnPosition;
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Saves last position before changing scene
    public void SavePositionForCurrentScene(Vector2 position)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        scenePositions[currentSceneName] = position;
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
