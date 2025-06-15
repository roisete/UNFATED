using UnityEngine;

public class BossDef : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;

    void Awake()
    {
        if (IsPlantDefeated() || CharacterStats.instance.difficulty == 1)
        {
            boss.SetActive(false);
        }
    }

    void Update()
    {
        //Desactivar cando se cambia a dificultade
        if (CharacterStats.instance.difficulty == 1)
        {
            boss.SetActive(false);
        }
        else if (CharacterStats.instance.difficulty != 1 && !IsPlantDefeated())
        {
            //Reactivar se se cambia de dificultade pero se non se derroutou antes
            boss.SetActive(true);
        }
    }

    //Facelo dinámico para reusalo con diferentes bosses
    bool IsPlantDefeated()
    {
        return CharacterEvents.instance.plantDefeated;
    }
}
