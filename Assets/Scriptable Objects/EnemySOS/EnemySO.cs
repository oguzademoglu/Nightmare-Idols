using UnityEngine;

[CreateAssetMenu(menuName = "Enemies", fileName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public string enemyType;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public float enemyXP;
    [SerializeField] public RuntimeAnimatorController animatorController;

}
