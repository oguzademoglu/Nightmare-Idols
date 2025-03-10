using UnityEngine;

public class HammerWeapon : MonoBehaviour
{
    public WeaponSO weaponData;
    Transform player;
    float currentAngle;
    float attackTimer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = weaponData.attackCooldown;
    }

    void Update()
    {
        HandleWeapon();
    }

    void HandleWeapon()
    {
        if (weaponData == null || player == null) return;
        currentAngle += weaponData.projectileSpeed * Time.deltaTime;
        float radians = currentAngle * Mathf.Deg2Rad;
        // transform.position = new Vector2(player.position.x + (Mathf.Cos(radians) * weaponData.range), player.position.y + MathF.Sin(radians) * weaponData.range);
        if (currentAngle == 0) currentAngle = 90f;
        Vector2 offset = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * weaponData.range;
        transform.position = (Vector2)player.position + offset;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = weaponData.attackCooldown;
            DealDamage();
        }
    }

    void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.explosionRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(weaponData.weaponDamage); // WeaponSO'dan hasarı al
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponData.explosionRadius);
    }
}
