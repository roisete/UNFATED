using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text hpText;

    public void SetHUD()
    {
        nameText.text = CharacterStats.instance.playerName;
        levelText.text = "Level: " + CharacterStats.instance.level.ToString();
        hpText.text = CharacterStats.instance.health.ToString() + "/" + CharacterStats.instance.maxHealth.ToString();
    }
}
