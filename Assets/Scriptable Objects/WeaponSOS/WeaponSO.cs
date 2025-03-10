using UnityEngine;

public enum WeaponType { Melee, Ranged, AoE, Rotating, FixedArea }
public enum StatusEffect { None, Burn, Freeze, Poison, Stun }

[CreateAssetMenu(menuName = "Weapons", fileName = "New Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("General Info")]
    [SerializeField] public string weaponName;
    [SerializeField] public GameObject weaponPrefab;
    [SerializeField] public WeaponType weaponType;


    [Header("Stats")]
    [SerializeField] public float weaponDamage;
    [SerializeField] public float weaponLevel;
    [SerializeField] public float attackCooldown;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float range;
    [SerializeField] public float spreadCount;
    [SerializeField] public int pierceCount; // kaç düşmanı delip geçer falan...


    [Header("Effects")]
    [SerializeField] public bool isAoEWeapon;
    [SerializeField] public float explosionRadius;
    [SerializeField] public float effectDuration;
    [SerializeField] public StatusEffect statusEffect;
    [SerializeField] public bool causesBurn;
    [SerializeField] public bool causesSlow;
    [SerializeField] public bool affectsEntireScreen;
    [SerializeField] public ParticleSystem effectPrefab;
    [SerializeField] public RuntimeAnimatorController animatorController;


    [Header("Evolution System")]
    [SerializeField] public bool canEvolve;
    [SerializeField] public WeaponSO evolvedWeapon;
}
