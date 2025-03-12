using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public float maxHealth = 10f;
    [SerializeField] public Slider healthSlider;
    public float currentHealth;

    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Debug.Log(currentHealth);
        if (currentHealth <= 0) Debug.Log("Player is Dead");
        healthSlider.value = currentHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
