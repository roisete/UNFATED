using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private Vector2 initialSpawnPointInTargetScene;
    
    private void Start()
    {
        // Set the initial spawn point for the target scene if it hasn't been set
        if (SceneSpawnManager.instance != null)
        {
            SceneSpawnManager.instance.SetInitialSpawnPoint(targetSceneName, initialSpawnPointInTargetScene);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Use the new scene management system
            if (SceneSpawnManager.instance != null)
            {
                SceneSpawnManager.instance.ChangeScene(targetSceneName);
            }
        }
    }
}
