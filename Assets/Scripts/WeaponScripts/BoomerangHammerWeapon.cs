using UnityEngine;

public class BoomerangHammerWeapon : WeaponBase
{
    [System.Obsolete]
    protected override void Attack()
    {
        GameObject projectile = Instantiate(weaponData.weaponPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        // Vector2 fireDirection = plyaer.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.velocity = transform.right * weaponData.projectileSpeed;
        Destroy(projectile, 3f);
    }
}
