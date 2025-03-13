using System;
using System.Collections;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponSO weaponData;
    Transform player;
    float rotationSpeed = 100f;
    public bool canAttack = true;

    public float orbitRadius = 1.5f;

    [Obsolete]
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AutoAttack());
    }

    [Obsolete]
    IEnumerator AutoAttack()
    {
        while (true)
        {
            if (canAttack)
            {
                Attack();
                // yield return new WaitForSeconds(weaponData.attackCooldown);
            }
            yield return null;
        }
    }

    [Obsolete]
    void Update()
    {
        if (weaponData.weaponType == WeaponType.Rotating) RotateAroundPlayer();
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

        // if (weaponData.weaponPrefab)
        // {
        //     Vector2 fireDirection = player.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        //     for (int i = 0; i < weaponData.spreadCount; i++)
        //     {
        //         GameObject projectile = Instantiate(weaponData.weaponPrefab, transform.position, Quaternion.identity);
        //         Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        //         if (rb)
        //         {
        //             float spreadAngle = (1 - weaponData.spreadCount / 2) * 10f; //yayılma açısı
        //             Vector2 direction = Quaternion.Euler(0, 0, spreadAngle) * fireDirection;
        //             rb.velocity = direction * weaponData.projectileSpeed;
        //             projectile.GetComponent<Bullet>().SetPierceCount(weaponData.pierceCount);
        //         }
        //     }
        // }
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
