using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject objet;
    [SerializeField]
    private Vector2 distance;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            objet.transform.position = new Vector3(target.transform.position.x + distance.x, target.transform.position.y + distance.y);
    }
}
