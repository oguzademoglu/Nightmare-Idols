using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hitWaitTime = .5f;
    float hitCounter;

    float currentHealth;

    Transform player;
    float speed;
    float health;
    float damage;
    float enemyXP;
    Animator animator;
    SpriteRenderer spriteRenderer;
    EnemyPool enemyPool;

    public void Initiliaze(EnemySO enemySO, Transform playerTransform, EnemyPool pool)
    {
        speed = enemySO.speed;
        damage = enemySO.damage;
        enemyXP = enemySO.enemyXP;
        health = enemySO.health;
        currentHealth = health;

        player = playerTransform;

        enemyPool = pool;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (animator == null) animator = gameObject.AddComponent<Animator>();
        if (enemySO.animatorController != null) animator.runtimeAnimatorController = enemySO.animatorController;
    }

    void FixedUpdate()
    {
        MoveEnemy();

        if (hitCounter > 0f) hitCounter -= Time.deltaTime;
    }

    void MoveEnemy()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
            if (player.position.x < transform.position.x) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
            EnemyAnimatorController(true);
        }
        else
        {
            EnemyAnimatorController(false);
        }
    }

    void EnemyAnimatorController(bool checkMove)
    {
        if (animator) animator.SetBool("isMoving", checkMove);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlayerXP playerXP = FindAnyObjectByType<PlayerXP>();
            if (playerXP != null)
            {
                playerXP.GainXP(enemyXP);
            }
            enemyPool.ReturnObject(gameObject);

            ResetEnemy();
        }
    }

    void ResetEnemy()
    {
        currentHealth = health;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            player.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }


}
