using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneSpawnData
    {
        public string sceneName;
        public Vector2 initialSpawnPoint;    // Fixed spawn point for first entry
        public Vector2 lastPosition;         // Last position in the scene
        public bool hasVisited;             // Track if scene was visited before
        public float deviationRange;
    }

    [SerializeField]
    private List<SceneSpawnData> sceneSpawnDataList = new List<SceneSpawnData>();
    
    public static SceneSpawnManager instance;
    
    // Store custom spawn coordinates for the next scene change
    private Vector2? customSpawnCoordinates = null;
    private bool useCustomSpawn = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 spawnPosition;
            
            // Check if custom spawn coordinates were provided
            if (useCustomSpawn && customSpawnCoordinates.HasValue)
            {
                spawnPosition = customSpawnCoordinates.Value;
                // Reset custom spawn flags
                useCustomSpawn = false;
                customSpawnCoordinates = null;
            }
            else
            {
                // Use existing spawn logic
                SceneSpawnData spawnData = sceneSpawnDataList.Find(sd => sd.sceneName == scene.name);
                if (spawnData != null)
                {
                    if (!spawnData.hasVisited)
                    {
                        // First time visiting - use initial spawn point
                        spawnPosition = spawnData.initialSpawnPoint;
                        spawnData.hasVisited = true;
                    }
                    else
                    {
                        // Subsequent visits - use last position with deviation
                        spawnPosition = spawnData.lastPosition + new Vector2(0, -spawnData.deviationRange);
                    }
                }
                else
                {
                    // If no spawn data exists, use player's current position
                    spawnPosition = player.transform.position;
                }
            }
            
            player.transform.position = spawnPosition;
        }
    }

    public void SaveCurrentPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneSpawnData spawnData = sceneSpawnDataList.Find(sd => sd.sceneName == currentSceneName);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (spawnData != null)
            {
                spawnData.lastPosition = player.transform.position;
            }
            else
            {
                // If no data exists for this scene, create it
                SceneSpawnData newData = new SceneSpawnData
                {
                    sceneName = currentSceneName,
                    initialSpawnPoint = player.transform.position,
                    lastPosition = player.transform.position,
                    hasVisited = true
                };
                sceneSpawnDataList.Add(newData);
            }
        }
    }

    public void SetInitialSpawnPoint(string sceneName, Vector2 spawnPoint)
    {
        SceneSpawnData spawnData = sceneSpawnDataList.Find(sd => sd.sceneName == sceneName);
        if (spawnData != null)
        {
            spawnData.initialSpawnPoint = spawnPoint;
        }
        else
        {
            SceneSpawnData newData = new SceneSpawnData
            {
                sceneName = sceneName,
                initialSpawnPoint = spawnPoint,
                lastPosition = spawnPoint,
                hasVisited = false
            };
            sceneSpawnDataList.Add(newData);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SaveCurrentPosition();
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void ChangeScene(string sceneName, Vector2 spawnCoordinates)
    {
        SaveCurrentPosition();
        customSpawnCoordinates = spawnCoordinates;
        useCustomSpawn = true;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
