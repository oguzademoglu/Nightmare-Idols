using UnityEngine;

public class EnergyRingWeapon : WeaponBase
{
    [System.Obsolete]
    protected override void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")) enemy.GetComponent<Enemy>().TakeDamage(weaponData.weaponDamage);
        }
    }
}
