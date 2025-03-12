using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPUIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Slider xpBar;

    PlayerXP playerXP;

    void Start()
    {
        Invoke(nameof(FindPlayerXP), 0.5f);
    }

    void FindPlayerXP()
    {
        playerXP = FindAnyObjectByType<PlayerXP>();
        playerXP.LevelUpEvent += UpdateUI;

        UpdateUI(playerXP.level);
    }

    void Update()
    {
        if (playerXP != null) xpBar.value = playerXP.currentXP / playerXP.xpToNextLevel;
    }


    void UpdateUI(int level)
    {
        levelText.text = $"LVL  {level}";
    }

}
