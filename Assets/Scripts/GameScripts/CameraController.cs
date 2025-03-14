using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    
    // Reference to the player
    private Transform playerTransform;

    void Awake()
    {
        // Singleton implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            // Find the player
            FindAndSetPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the player again after scene change
        FindAndSetPlayer();
    }
    
    // Find the player and set it as the follow target
    private void FindAndSetPlayer()
    {
        // Try to find the player using the PlayerController singleton
        if (PlayerController.instance != null)
        {
            playerTransform = PlayerController.instance.transform;
            
            // Set the player as the follow target for the virtual camera
            if (virtualCamera != null)
            {
                virtualCamera.Follow = playerTransform;
                virtualCamera.LookAt = playerTransform;
            }
        }
        else
        {
            // If player not found, try again after a short delay
            StartCoroutine(FindPlayerWithDelay());
        }
    }
    
    // Coroutine to find the player with a delay
    private IEnumerator FindPlayerWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        FindAndSetPlayer();
    }
    
    // Called when the object is destroyed
    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // Public method to manually set the follow target
    public void SetFollowTarget(Transform target)
    {
        if (virtualCamera != null && target != null)
        {
            playerTransform = target;
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
        }
    }
}