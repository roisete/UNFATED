using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private CinemachineConfiner cinemachineConfiner;

    // Reference to the player
    private Transform playerTransform;

    private Vector2 posMenu = Vector3.zero;

    void Awake()
    {
        // Find the player when the scene loads
        FindAndSetPlayer();

        // Subscribe to the sceneLoaded event to handle scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a new scene is loaded, find the player again
        FindAndSetPlayer();

        // Update the CinemachineConfiner with the new scene's collider
        UpdateCinemachineConfiner();
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

    // Update the CinemachineConfiner with the new scene's collider
    private void UpdateCinemachineConfiner()
    {
        if (cinemachineConfiner != null)
        {
            // Find the new boundary collider in the scene
            PolygonCollider2D newBoundary = GameObject.FindWithTag("CameraBoundary")?.GetComponent<PolygonCollider2D>();

            if (newBoundary != null)
            {
                // Update the CinemachineConfiner with the new boundary
                cinemachineConfiner.m_BoundingShape2D = newBoundary;
            }
        }
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