using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponBase> equippedWeapons = new();
    public Transform weaponHolder;
    const int maxWeapons = 6;

    void Update()
    {
        HandleWeaponCooldowns();
    }

    private void HandleWeaponCooldowns()
    {
        foreach (var weapon in equippedWeapons)
        {
            if (weapon != null) weapon.enabled = true; // Silah kendi cooldown süresine göre saldıracak
        }
    }

    public void EquipWeapon(WeaponSO newWeapon)
    {
        if (equippedWeapons.Count >= maxWeapons)
        {
            Debug.Log("Maksimum silah kapasitesine ulaşıldı!");
            return;
        }

        GameObject weaponObject = Instantiate(newWeapon.weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        WeaponBase weaponBase = weaponObject.GetComponent<WeaponBase>();

        if (weaponBase != null)
        {
            weaponBase.weaponData = newWeapon;
            equippedWeapons.Add(weaponBase);
            Debug.Log($"{newWeapon.weaponName} envantere eklendi!");
        }
        else
        {
            Debug.LogError("WeaponBase bileşeni eksik! WeaponPrefab doğru atanmış mı?");
            Destroy(weaponObject);
        }
    }

    public void RemoveWeapon(WeaponBase weapon)
    {
        if (equippedWeapons.Contains(weapon))
        {
            equippedWeapons.Remove(weapon);
            Destroy(weapon.gameObject);
            Debug.Log($"{weapon.weaponData.weaponName} envanterden çıkarıldı!");
        }
    }

    public void UpgradeWeapon(WeaponSO weapon)
    {
        weapon.weaponLevel++;
        weapon.weaponDamage *= 1.1f;
    }

    public List<WeaponBase> GetWeapons()
    {
        return equippedWeapons;
    }

    internal List<WeaponSO> GetAwailableWeapons()
    {
        throw new NotImplementedException();
    }
}
