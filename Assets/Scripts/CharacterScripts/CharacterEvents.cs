using UnityEngine;

public class CharacterEvents : MonoBehaviour
{
    public bool caveOpenedTriggered = false;
    public bool robotinWalkTriggered = false;
    public bool plantDefeated = false;
    public bool puzzleResolved = false;


    public static CharacterEvents instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Se existe outra instancia, destruíla para evitar duplicados
            Destroy(gameObject);
        }
    }
}
