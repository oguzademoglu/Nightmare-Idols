using System.Collections;
using UnityEngine;

public class FireballWeapon : WeaponBase
{
    [System.Obsolete]
    protected override void Attack()
    {
        if (!canAttack) return;
        Debug.Log("Attack çalıştı");
        GameObject projectile = Instantiate(weaponData.weaponPrefab, transform.position, Quaternion.identity);
        // Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        // rb.velocity = transform.right * weaponData.projectileSpeed;

        if (projectile.TryGetComponent<FireballProjectile>(out var fireballProjectile))
        {
            Debug.Log("fireball oluşturuldu");
            SpriteRenderer spriteRenderer = GetComponentInParent<SpriteRenderer>();
            float direction = spriteRenderer.flipX ? -1f : 1f;
            fireballProjectile.SetStats(weaponData.projectileSpeed, weaponData.weaponDamage, weaponData.range, weaponData.explosionRadius, direction);
        }
        Debug.Log("Fireball fired");
        StartCoroutine(StartCoolDown());
    }

    IEnumerator StartCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(weaponData.attackCooldown);
        canAttack = true;
    }
}
