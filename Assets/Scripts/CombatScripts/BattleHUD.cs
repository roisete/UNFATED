using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text levelText;
    public Text hpText;
    public Text mpText;

    public void SetHUD()
    {
        levelText.text = CharacterStats.instance.level.ToString();
        hpText.text = CharacterStats.instance.health.ToString() + "/" + CharacterStats.instance.maxHealth.ToString();
        mpText.text = CharacterStats.instance.mana.ToString() + "/" + CharacterStats.instance.maxMana.ToString();
    }
}
