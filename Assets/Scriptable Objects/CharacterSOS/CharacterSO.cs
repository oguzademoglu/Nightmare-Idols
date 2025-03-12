using UnityEngine;

[CreateAssetMenu(menuName = "Characters", fileName = "New Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public float health;
    public float speed;
    public WeaponSO defaultWeapon;
}
