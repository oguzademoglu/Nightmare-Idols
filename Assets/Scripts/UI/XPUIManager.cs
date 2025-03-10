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
        playerXP = FindAnyObjectByType<PlayerXP>();
        playerXP.LevelUpEvent += UpdateUI;

        UpdateUI(playerXP.level);
    }


    void Update()
    {
        xpBar.value = playerXP.currentXP / playerXP.xpToNextLevel;
    }


    void UpdateUI(int level)
    {
        levelText.text = $"LVL  {level}";
    }

}
