using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    [SerializeField] public float maxHealth;
    [SerializeField] public Slider healthSlider;
    public float currentHealth;

    void Awake()
    {
        instance = this;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Debug.Log(currentHealth);
        if (currentHealth <= 0) Debug.Log("Player is Dead");
        healthSlider.value = currentHealth;
    }

    internal void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
