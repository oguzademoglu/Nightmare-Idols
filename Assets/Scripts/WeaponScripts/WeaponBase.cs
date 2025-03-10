using System;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponSO weaponData;
    Transform player;
    float cooldownTimer = 0f;
    float rotationSpeed = 100f;

    public float orbitRadius = 1.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    [Obsolete]
    void Update()
    {
        if (cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = weaponData.attackCooldown;
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (weaponData.weaponType == WeaponType.Rotating)
        {
            RotateAroundPlayer();
        }
    }



    [Obsolete]
    protected virtual void Attack()
    {
        switch (weaponData.weaponType)
        {
            case WeaponType.Melee:
                MeleeAttack();
                break;

            case WeaponType.Ranged:
                RangedAttack();
                break;
            case WeaponType.AoE:
                AoEAttack();
                break;
            case WeaponType.Rotating:
                RotatingAttack();
                break;
            case WeaponType.FixedArea:
                FixedAreaAttack();
                break;
        }
    }

    private void FixedAreaAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(weaponData.weaponDamage);
                // Debug.Log($"{weaponData.weaponName} sabit alanda {enemy.name} düşmanına saldırdı!");
            }
        }
    }

    private void RotatingAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(weaponData.weaponDamage);
                // Debug.Log($"{weaponData.weaponName} dönerken {enemy.name} düşmanına vurdu!");
            }
        }
    }

    private void AoEAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.explosionRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(weaponData.weaponDamage);
                // Debug.Log($"{weaponData.weaponName} melee attack, {enemy.name}, {weaponData.weaponDamage}");
            }
        }
    }

    [Obsolete]
    private void RangedAttack()
    {
        if (weaponData.weaponPrefab)
        {
            GameObject projectile = Instantiate(weaponData.weaponPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.velocity = transform.right * weaponData.projectileSpeed;
            }
        }
        // Debug.Log($"{weaponData.weaponName} menzilli saldırı yaptı!");
    }

    private void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(weaponData.weaponDamage);
                // Debug.Log($"{weaponData.weaponName} melee attack, {enemy.name}, {weaponData.weaponDamage}");
            }

        }
        // Debug.Log($"{weaponData.weaponName} melee saldırısı yaptı!");
    }


    private void RotateAroundPlayer()
    {
        if (player == null) return;
        Vector3 orbitPosition = player.position + (transform.right * orbitRadius);
        transform.position = orbitPosition;
        transform.RotateAround(player.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponData.range);
    }
}
