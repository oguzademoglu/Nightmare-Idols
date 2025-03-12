using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpPanel;
    public Button[] optionButtons;

    List<WeaponSO> availableWapons;
    PlayerWeaponManager playerWeaponManager;
    void Start()
    {
        playerWeaponManager = FindAnyObjectByType<PlayerWeaponManager>();
        levelUpPanel.SetActive(false);
    }

    public void ShowLevelUpOptions()
    {
        levelUpPanel.SetActive(true);
        availableWapons = GetRandomWeaponChoices();

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < availableWapons.Count)
            {
                WeaponSO weapon = availableWapons[i];
                optionButtons[i].GetComponentInChildren<Text>().text = weapon.name;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectWeapon(weapon));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }

        }
    }

    private void SelectWeapon(WeaponSO selectedWeapon)
    {
        playerWeaponManager.EquipWeapon(selectedWeapon);
        levelUpPanel.SetActive(false);
    }

    public List<WeaponSO> GetRandomWeaponChoices()
    {
        List<WeaponSO> weapons = playerWeaponManager.GetAwailableWeapons();
        List<WeaponSO> choices = new();
        while (choices.Count < 3 && weapons.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, weapons.Count);
            choices.Add(weapons[randomIndex]);
            weapons.RemoveAt(randomIndex);
        }
        return choices;
    }
}
