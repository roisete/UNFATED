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

    //Referencia ao player
    private Transform playerTransform;

    private Vector2 posMenu = Vector3.zero;

    void Awake()
    {
        FindAndSetPlayer();
        //Configura a camera para seguir ao xogador
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetPlayer();
        UpdateCinemachineConfiner();
    }

    //Encontra ao xogador cando se carga a escena
    private void FindAndSetPlayer()
    {
        if (PlayerController.instance != null)
        {
            playerTransform = PlayerController.instance.transform;

            //Configura a cámara para seguir ao xogador
            if (virtualCamera != null)
            {
                virtualCamera.Follow = playerTransform;
                virtualCamera.LookAt = playerTransform;
            }
        }
        else
        {
            //Se non se encontra, vólveo intentar
            StartCoroutine(FindPlayerWithDelay());
        }
    }

    private IEnumerator FindPlayerWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        FindAndSetPlayer();
    }

    //Actualiza o collider da cámara
    private void UpdateCinemachineConfiner()
    {
        if (cinemachineConfiner != null)
        {
            PolygonCollider2D newBoundary = GameObject.FindWithTag("CameraBoundary")?.GetComponent<PolygonCollider2D>();

            if (newBoundary != null)
            {
                //Actualiza o CinemachineConfiner
                cinemachineConfiner.m_BoundingShape2D = newBoundary;
            }
        }
    }

    void OnDestroy()
    {
        //Desactiva a cámara
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Método manual
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