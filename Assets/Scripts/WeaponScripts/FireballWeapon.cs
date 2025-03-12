using UnityEngine;

public class FireballWeapon : WeaponBase
{
    [System.Obsolete]
    protected override void Attack()
    {
        GameObject projectile = Instantiate(weaponData.weaponPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * weaponData.projectileSpeed;
    }
}
