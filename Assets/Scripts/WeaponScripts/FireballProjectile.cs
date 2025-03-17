using System;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    float speed;
    float damage;
    float range;
    float direction;
    Vector3 startPosition;
    float explosionRadius;

    public void SetStats(float spd, float dmg, float rng, float expRad, float drc)
    {

        speed = spd;
        damage = dmg;
        range = rng;
        explosionRadius = expRad;
        direction = drc;
        startPosition = transform.position;
    }


    void Update()
    {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        if (Vector3.Distance(startPosition, transform.position) >= range) Explode();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) Explode();
    }

    void Explode()
    {
        // if (effectPrefab != null)
        // {
        //     Instantiate(effectPrefab, transform.position, Quaternion.identity);
        // }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        Destroy(gameObject, 0.1f);
    }
}
