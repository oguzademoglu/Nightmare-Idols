using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveSpeed;
    WeaponSO defaultWeapon;

    bool isRunning;
    Vector2 moveInput;
    Rigidbody2D playerRb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerWeaponManager playerWeaponManager;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    public void InitializeCharacter(CharacterSO characterData)
    {
        moveSpeed = characterData.speed;
        defaultWeapon = characterData.defaultWeapon;
        playerWeaponManager.EquipWeapon(defaultWeapon);
    }

    [Obsolete]
    void FixedUpdate()
    {
        MovePlayer();
        AnimatorControllers();

    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }

    [Obsolete]
    void MovePlayer()
    {
        Vector2 movement = new(moveInput.x * moveSpeed * Time.fixedDeltaTime, moveInput.y * moveSpeed * Time.fixedDeltaTime);
        playerRb.velocity = movement;
        if (movement.x != 0 || movement.y != 0) isRunning = true;
        else isRunning = false;
        if (movement.x > 0) spriteRenderer.flipX = false;
        else if (movement.x < 0) spriteRenderer.flipX = true;
    }


    public void AnimatorControllers()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetFloat("dead", PlayerHealth.instance.currentHealth);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void EquipNewWeapon(WeaponSO newWeapon)
    {
        if (playerWeaponManager != null) playerWeaponManager.EquipWeapon(newWeapon);
    }
}
